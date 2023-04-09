using Microsoft.EntityFrameworkCore.Storage;


namespace JF.Utils.Infrastructure.Persistence
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
        IRepository<TEntity>? Repository<TEntity>() where TEntity : class;
        IReadRepository<TEntity>? ReadRepository<TEntity>() where TEntity : class;

    }
}
