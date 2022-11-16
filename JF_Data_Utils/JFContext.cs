using JF.Utils.Data.Helper;
using JF.Utils.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JF.Utils.Data
{
    public class JFContext : DbContext, IUnitOfWork
    {
        private readonly string? _username;

        private IDbContextTransaction? _currentTransaction;
        public IDbContextTransaction? GetCurrentTransaction() => _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;
        public JFContext(DbContextOptions<JFContext> options, string username) : base(options)
        {
            _username = username;
        }

        public JFContext(DbContextOptions options) : base(options)
        {
            _username = "Generic";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SetQueryFilterOnAllEntities<IEntitySoftDelete>(e => !e.IsDeleted);
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            UpdateSoftDelete();
            UpdateAuditable();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            UpdateSoftDelete();
            UpdateAuditable();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateSoftDelete()
        {
            foreach (var entry in ChangeTracker.Entries())
                if (entry.Entity.GetType().GetInterfaces().Contains(typeof(IEntitySoftDelete)))
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.CurrentValues["DeletedDate"] = false;
                            entry.CurrentValues["DeletedBy"] = false;
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
            foreach (var entry in ChangeTracker.Entries())
                if (entry.Entity.GetType().GetInterfaces().Contains(typeof(IEntityAuditable)))
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
                            entry.CurrentValues["LastModifiedDate"] = DateTime.Now;
                            entry.CurrentValues["LastModifiedBy"] = _username;
                            break;
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
            try
            {
                SaveChanges();
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
    }
}
