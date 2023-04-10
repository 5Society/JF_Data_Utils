using JF.Utils.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;


namespace JF.Utils.Infrastructure.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        public IDbContextTransaction? GetCurrentTransaction();
        bool HasActiveTransaction { get; }
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<int> CommitTransactionAsync(CancellationToken cancellationToken = default);
        void RollbackTransaction();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void AddRepository<TEntity>(object repository);
        IRepository<TEntity>? Repository<TEntity>() where TEntity : class, IAggregateRoot;
        IReadRepository<TEntity>? ReadRepository<TEntity>() where TEntity : class, IAggregateRoot;

    }
}
