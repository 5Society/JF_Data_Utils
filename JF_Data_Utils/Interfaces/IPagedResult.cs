using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JF.Utils.Data.Interfaces
{
 
    public interface IPagedResult<TEntity> where TEntity : class
    {
        int CurrentPage { get; }
        int PageCount { get;  }
        int PageSize { get; }
        int RowCount { get; }
        IList<TEntity> GetResults();

    }
}