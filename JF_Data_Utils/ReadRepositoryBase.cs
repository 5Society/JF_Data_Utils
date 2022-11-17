using JF.Utils.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JF.Utils.Data
{
    public class ReadRepositoryBase<TEntity> : IReadRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly DbSet<TEntity> _entities;
        protected readonly JFContext _context;
        public IUnitOfWork UnitOfWork { get { return _context; } }
        

        public ReadRepositoryBase(IUnitOfWork context)
        {
            _context = (JFContext)(context ?? throw new ArgumentNullException(nameof(context)));
            _entities = _context.Set<TEntity>();
        }
        

        public virtual async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return await _entities.AnyAsync(cancellationToken);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _entities.AnyAsync(predicate, cancellationToken);
        }

        public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await _entities.CountAsync(cancellationToken);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _entities.Where(predicate).CountAsync(cancellationToken);
        }

        public virtual IQueryable<TEntity> GetAll(bool asNoTracking = true)
        {
            return asNoTracking ? _entities.AsNoTracking() 
                : _entities.AsQueryable();
        }

        public virtual IQueryable<TEntity> GetAllBySpec(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true)
        {
            return asNoTracking ? _entities.Where(predicate).AsNoTracking()
                :_entities.Where(predicate).AsQueryable();
        }

        public virtual IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = GetAll();
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include(includeProperty);
            }

            return queryable;
        }

        public virtual async Task<TEntity?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
        {
            return await _entities.FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        }

        public virtual async Task<TEntity?> GetBySpecAsync<Spec>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _entities.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public virtual async Task<ICollection<TEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await _entities.ToListAsync(cancellationToken);
        }

        public virtual async Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _entities.Where(predicate).ToListAsync(cancellationToken);
        }
    }
}
