using JF.Utils.Application.Persistence;
using JF.Utils.Domain.Entities;
using JF.Utils.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;

namespace JF.Utils.Infrastructure.Persistence
{
    /// <summary>
    /// Implementación de DbContext que proporciona funcionalidades adicionales para el trabajo con Entity Framework Core.
    /// También implementa la interfaz IUnitOfWork para gestionar transacciones y repositorios.
    /// </summary>
    public class JFContext : DbContext, IUnitOfWork
    {
        private readonly string? _username;

        private readonly Dictionary<string, dynamic> _repositoriesBase = new();

        private readonly Dictionary<string, dynamic> _repositoriesRead = new();

        private IDbContextTransaction? _currentTransaction;

        /// <summary>
        /// Obtiene la transacción de contexto actual, si existe.
        /// </summary>
        /// <returns>La transacción de contexto actual o null si no hay ninguna.</returns>
        public IDbContextTransaction? GetCurrentTransaction() => _currentTransaction;

        /// <summary>
        /// Indica si hay una transacción activa en el contexto.
        /// </summary>
        public bool HasActiveTransaction => _currentTransaction != null;

        /// <summary>
        /// Constructor que acepta opciones de contexto y el nombre de usuario asociado al contexto.
        /// </summary>
        /// <param name="options">Opciones de contexto de Entity Framework Core.</param>
        /// <param name="username">Nombre de usuario asociado al contexto.</param>
        public JFContext(DbContextOptions<JFContext> options, string username) : base(options)
        {
            _username = username;
        }

        /// <summary>
        /// Constructor que acepta solo opciones de contexto y establece un nombre de usuario genérico.
        /// </summary>
        /// <param name="options">Opciones de contexto de Entity Framework Core.</param>
        public JFContext(DbContextOptions<JFContext> options) : base(options)
        {
            _username = "Generic";
        }

        /// <summary>
        /// Método llamado para configurar el modelo de datos, incluyendo filtros de consulta por defecto.
        /// </summary>
        /// <param name="modelBuilder">Constructor de modelos de Entity Framework Core.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplica un filtro de consulta por defecto para entidades que implementan ISoftDeleteEntity.
            modelBuilder.SetQueryFilterOnAllEntities<ISoftDeleteEntity>(e => e.DeletedDate == null);
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Guarda todos los cambios realizados en el contexto en la base de datos de forma asincrónica.
        /// Además, realiza validaciones y actualizaciones adicionales antes de guardar los cambios.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Número de entidades afectadas.</returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
       => await SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken);


        /// <summary>
        /// Guarda todos los cambios realizados en el contexto en la base de datos de forma asincrónica.
        /// Además, realiza validaciones y actualizaciones adicionales antes de guardar los cambios.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Indica si se deben aceptar todos los cambios en caso de éxito.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Número de entidades afectadas.</returns>
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
            foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity.GetType().GetInterfaces().Contains(typeof(ISoftDeleteEntity))))
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
            foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity.GetType().GetInterfaces().Contains(typeof(IAuditableEntity))))
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
        /// <summary>
        /// Inicia una transacción de base de datos de forma asincrónica.
        /// </summary>
        /// <returns>La transacción de base de datos iniciada.</returns>
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null!;
            _currentTransaction = await Database.BeginTransactionAsync();
            return _currentTransaction;
        }

        /// <summary>
        /// Confirma la transacción de base de datos actual de forma asincrónica.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Número de entidades afectadas.</returns>
        public async Task<int> CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            int result = 0;
            if (_currentTransaction == null) return result;
            try
            {
                result = await SaveChangesAsync(cancellationToken);
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

        /// <summary>
        /// Revierte la transacción de base de datos actual.
        /// </summary>
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

        /// <summary>
        /// Obtiene un repositorio base para la entidad especificada.
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad.</typeparam>
        /// <returns>Repositorio para la entidad especificada.</returns>
        public IRepository<TEntity>? Repository<TEntity>()
            where TEntity : class, IAggregateRoot
        {
            var type = typeof(TEntity).Name;
            if (_repositoriesBase.TryGetValue(type, out var repository)) return repository;
            if (_repositoriesRead.ContainsKey(type)) return null;
            _repositoriesBase.Add(type, new JFRepository<TEntity>(this));
            return _repositoriesBase[type];
        }

        /// <summary>
        /// Obtiene un repositorio de solo lectura para la entidad especificada.
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad.</typeparam>
        /// <returns>Repositorio de solo lectura para la entidad especificada.</returns>
        public IReadRepository<TEntity>? ReadRepository<TEntity>()
            where TEntity : class, IAggregateRoot
        {
            var type = typeof(TEntity).Name;
            if (_repositoriesBase.TryGetValue(type, out var repository)) return (IReadRepository<TEntity>)repository;
            if (_repositoriesRead.TryGetValue(type, out var repositoryRead)) return repositoryRead;
            _repositoriesRead.Add(type, new JFRepository<TEntity>(this));
            return _repositoriesRead[type];
        }

        /// <summary>
        /// Agrega un repositorio personalizado para la entidad especificada.
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad.</typeparam>
        /// <param name="repository">Instancia del repositorio personalizado.</param>
        public void AddRepository<TEntity>(object repository)
        {
            var type = typeof(TEntity).Name;
            if (_repositoriesBase.ContainsKey(type)) throw new InvalidOperationException("Repository already exists");
            _repositoriesBase.Add(type, repository);
        }
    }
}
