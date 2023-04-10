using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;
using JF.Utils.Domain.Entities;
using JF.Utils.Infrastructure.Extensions;

namespace JF.Utils.Infrastructure.Persistence
{
    public class JFContext : DbContext, IUnitOfWork
    {
        private readonly string? _username;

        private readonly Dictionary<string, dynamic> _repositoriesBase = new Dictionary<string, dynamic>();

        private readonly Dictionary<string, dynamic> _repositoriesRead = new Dictionary<string, dynamic>();

        private IDbContextTransaction? _currentTransaction;
        public IDbContextTransaction? GetCurrentTransaction() => _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;
        public JFContext(DbContextOptions<JFContext> options, string username) : base(options)
        {
            _username = username;
        }

        public JFContext(DbContextOptions<JFContext> options) : base(options)
        {
            _username = "Generic";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SetQueryFilterOnAllEntities<IEntitySoftDelete>(e => e.DeletedDate == null);
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken);


        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ValidateUpdateEntities();
            UpdateSoftDelete();
            UpdateAuditable();
            ValidateModelEntity();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void ValidateUpdateEntities()
        {
            //Validates if the record to be modified exists in the database. If it does not exist, change the status to not persist it.
            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified))
                if (entry.GetDatabaseValues() == null) entry.State = EntityState.Unchanged;
        }
        private void UpdateSoftDelete()
        {
            foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity.GetType().GetInterfaces().Contains(typeof(IEntitySoftDelete))))
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["DeletedDate"] = null;
                        entry.CurrentValues["DeletedBy"] = null;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["DeletedDate"] = DateTime.Now;
                        entry.CurrentValues["DeletedBy"] = _username;
                        break;
                }
        }
        private void UpdateAuditable()
        {
            foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity.GetType().GetInterfaces().Contains(typeof(IEntityAuditable))))
            {
                PropertyValues? databaseValues = entry.GetDatabaseValues();
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["CreatedDate"] = DateTime.Now;
                        entry.CurrentValues["CreatedBy"] = _username;
                        entry.CurrentValues["LastModifiedDate"] = null;
                        entry.CurrentValues["LastModifiedBy"] = null;
                        break;
                    case EntityState.Modified:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["CreatedDate"] = databaseValues?["CreatedDate"];
                        entry.CurrentValues["CreatedBy"] = databaseValues?["CreatedBy"];
                        entry.CurrentValues["LastModifiedDate"] = DateTime.Now;
                        entry.CurrentValues["LastModifiedBy"] = _username;
                        break;
                }
            }
        }
        private void ValidateModelEntity()
        {
            //Load references
            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
                foreach (var reference in entry.References)
                    reference.Load();
            //Validates entity's model
            var entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Added
                               || e.State == EntityState.Modified
                           select e.Entity;
            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext);
            }
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null!;
            _currentTransaction = await Database.BeginTransactionAsync();
            return _currentTransaction;
        }

        public async Task<int> CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            int result = 0;
            if (_currentTransaction == null) return result;
            try
            {
                result= await SaveChangesAsync(cancellationToken);
                _currentTransaction.Commit();
                _currentTransaction.Dispose();
                _currentTransaction = null!;
            }
            catch
            {
                RollbackTransaction();
            }
            return result;
        }

        public void DetectChanges()
        {
            ChangeTracker.DetectChanges();
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null!;
                }
            }
        }

        public IRepository<TEntity>? Repository<TEntity>() 
            where TEntity : class, IAggregateRoot
        {
            var type = typeof(TEntity).Name;
            if (_repositoriesBase.TryGetValue(type, out var repository)) return repository;
            if (_repositoriesRead.ContainsKey(type)) return null;
            _repositoriesBase.Add(type, new JFRepository<TEntity>(this));
            return _repositoriesBase[type];
        }

        public IReadRepository<TEntity>? ReadRepository<TEntity>() 
            where TEntity : class, IAggregateRoot
        {
            var type = typeof(TEntity).Name;
            if (_repositoriesBase.TryGetValue(type, out var repository)) return (IReadRepository<TEntity>)repository;
            if (_repositoriesRead.TryGetValue(type, out var repositoryRead)) return repositoryRead;
            _repositoriesRead.Add(type, new JFRepository<TEntity>(this));
            return _repositoriesRead[type];
        }

        public void AddRepository<TEntity>(object repository)
        {
            var type = typeof(TEntity).Name;
            if (_repositoriesBase.ContainsKey(type)) throw new InvalidOperationException("Repository already exists");
            _repositoriesBase.Add(type, repository);
        }
    }
}
