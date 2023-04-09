using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JF.Utils.Data.Application.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public IDbContextTransaction? GetCurrentTransaction();
        bool HasActiveTransaction { get; }
        void DetectChanges();
        IDbContextTransaction BeginTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync();
        bool CommitTransaction();
        Task<bool> CommitTransactionAsync();
        void RollbackTransaction();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();

        void AddRepository<TEntity>(object repository);
        IRepositoryBase<TEntity>? Repository<TEntity>() where TEntity : class;
        IReadRepositoryBase<TEntity>? ReadRepository<TEntity>() where TEntity : class;

    }
}
