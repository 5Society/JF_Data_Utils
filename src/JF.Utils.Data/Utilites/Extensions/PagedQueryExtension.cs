using JF.Utils.Data.Utilites.Common;

namespace JF.Utils.Data.Utilites.Extensions
{
    /// <summary>
    /// Extension method for IQueryable<T> that returns one page of results set.
    /// </summary>
    public static class PagedQueryExtension
    {
        public static JFPagedResult<T> GetPaged<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            var result = new JFPagedResult<T>(query);
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();
            result.PageCount = (int)Math.Ceiling((double)result.RowCount / pageSize);
            return result;
        }
    }
}
