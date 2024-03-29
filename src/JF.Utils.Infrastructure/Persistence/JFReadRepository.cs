﻿using JF.Utils.Application.Persistence;
using JF.Utils.Domain.Entities;
using JF.Utils.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JF.Utils.Infrastructure.Persistence
{
    public class JFReadRepository<TEntity> : IReadRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly DbSet<TEntity> _entities;
        protected readonly JFContext _context;
        public IUnitOfWork UnitOfWork { get { return _context; } }


        public JFReadRepository(IUnitOfWork context)
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

        public IList<TEntity> GetAllPaged(int page, int pagesize)
        {
            return GetAll().GetPaged(page, pagesize).GetResults();
        }
        public virtual IQueryable<TEntity> GetAllBySpec(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true)
        {
            return asNoTracking ? _entities.Where(predicate).AsNoTracking()
                : _entities.Where(predicate).AsQueryable();
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

        public virtual async Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            return await _entities.FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        }

        public virtual TEntity? GetById(object id)
        {
            return _entities.Find(new object[] { id });
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
