using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JF.Utils.Data.Interfaces
{
    public interface IReadRepository<TEntity> : IReadRepositoryBase<TEntity> where TEntity : class { }

}
