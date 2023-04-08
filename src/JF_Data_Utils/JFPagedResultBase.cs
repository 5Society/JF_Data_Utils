using JF.Utils.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JF.Utils.Data
{
    public class JFPagedResult<TEntity> : IPagedResult<TEntity> where TEntity : class
    {
        public IQueryable<TEntity> Query { get; internal set; }
        public int CurrentPage { get; internal set; }
        public int PageCount { get; internal set; }
        public int PageSize { get; internal set; }
        public int RowCount { get; internal set; }
        public IList<TEntity> GetResults() { 
            return Query.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList(); 
        }

        public JFPagedResult(IQueryable<TEntity> query)
        {
            Query = query;
        }
    }
}