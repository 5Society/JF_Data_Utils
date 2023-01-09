using JF.Utils.Data.Extensions;
using JF.Utils.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;

namespace JF.Utils.Data
{
    public class JFContext : DbContext, IUnitOfWork
    {
        private readonly string? _username;

        private Dictionary<string, dynamic> _repositoriesBase;
        private Dictionary<string, dynamic> _repositoriesRead;

        private IDbContextTransaction? _currentTransaction;
        public IDbContextTransaction? GetCurrentTransaction() => _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;
        public JFContext(DbContextOptions<JFContext> options, string username) : base(options)
        {
            _username = username;
            _repositoriesBase = new Dictionary<string, dynamic>();
            _repositoriesRead = new Dictionary<string, dynamic>();
        }

        public JFContext(DbContextOptions<JFContext> options) : base(options)
        {
            _username = "Generic";
            _repositoriesBase = new Dictionary<string, dynamic>();
            _repositoriesRead = new Dictionary<string, dynamic>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SetQueryFilterOnAllEntities<IEntitySoftDelete>(e => e.DeletedDate==null);
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges() => SaveChanges(acceptAllChangesOnSuccess: true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ValidateUpdateEntities();
            UpdateSoftDelete();
            UpdateAuditable();
            ValidateModelEntity();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) 
            => await SaveChangesAsync(acceptAllChangesOnSuccess:true, cancellationToken);
        
      
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
            foreach (var entry in ChangeTracker.Entries().Where(e => (e.State == EntityState.Added) || (e.State == EntityState.Modified)))
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
        public IDbContextTransaction BeginTransaction()
        {
            if (_currentTransaction != null) return null!;
            _currentTransaction = Database.BeginTransaction();
            return _currentTransaction;
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null!;
            _currentTransaction = await Database.BeginTransactionAsync();
            return _currentTransaction;
        }

        public bool CommitTransaction()
        {
            if (_currentTransaction == null) return false;
            int changes;
            try
            {
                changes = SaveChanges();
                _currentTransaction.Commit();
                _currentTransaction.Dispose();
                _currentTransaction = null!;
            }
            catch
            {
                RollbackTransaction();
                return false;
            }
            return changes>0;
        }

        public async Task<bool> CommitTransactionAsync()
        {
            if (_currentTransaction == null) return false;
            try
            {
                await SaveChangesAsync();
                _currentTransaction.Commit();
                _currentTransaction.Dispose();
                _currentTransaction = null!;
            }
            catch
            {
                RollbackTransaction();
                return false;
            }
            return true;
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

        public IRepositoryBase<TEntity>? Repository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity).Name;
            if (_repositoriesBase.ContainsKey(type)) return _repositoriesBase[type];
            if (_repositoriesRead.ContainsKey(type)) return null;
            _repositoriesBase.Add(type, new JFRepositoryBase<TEntity>(this));
            return _repositoriesBase[type];
        }

        public IReadRepositoryBase<TEntity>? ReadRepository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity).Name;
            if (_repositoriesBase.ContainsKey(type)) return (IReadRepositoryBase<TEntity>)_repositoriesBase[type];
            if (_repositoriesRead.ContainsKey(type)) return _repositoriesRead[type];
            _repositoriesRead.Add(type, new JFRepositoryBase<TEntity>(this));
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
