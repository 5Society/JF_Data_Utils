﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JF.Utils.Data.Interfaces
{
    public interface IRepositoryBase<TEntity> : IReadRepositoryBase<TEntity> where TEntity : class
    {
        IUnitOfWork UnitOfWork { get; }

        TEntity Add(TEntity entity);

        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        ICollection<TEntity> AddRange(ICollection<TEntity> entities);

        Task<int> AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);

        void Delete(TEntity entity);

        Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        void DeleteRange(ICollection<TEntity> entities);

        Task<int> DeleteRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);

        void Update(TEntity entity);

        Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        bool ValidateEntityModel(TEntity entity);
    }
}
