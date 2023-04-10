using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;


namespace JF.Utils.Infrastructure.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        public IDbContextTransaction? GetCurrentTransaction();
        bool HasActiveTransaction { get; }
        void DetectChanges();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<bool> CommitTransactionAsync(CancellationToken cancellationToken = default);
        void RollbackTransaction();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void AddRepository<TEntity>(object repository);
        IRepository<TEntity>? Repository<TEntity>() where TEntity : class;
        IReadRepository<TEntity>? ReadRepository<TEntity>() where TEntity : class;

    }
}
