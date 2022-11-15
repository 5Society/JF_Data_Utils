using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JF.Utils.Data.Interfaces
{
    public interface IRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : class { }
}
