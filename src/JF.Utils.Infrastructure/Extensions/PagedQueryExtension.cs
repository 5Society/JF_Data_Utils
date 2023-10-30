

using JF.Utils.Application.Common;
using JF.Utils.Infrastructure.Common;

namespace JF.Utils.Infrastructure.Extensions
{
    /// <summary>
    /// Extension method for IQueryable<T> that returns one page of results set.
    /// </summary>
    public static class PagedQueryExtension
    {
        public static IPagedResult<T> GetPaged<T>(this IQueryable<T> query, int page, int pageSize)
            where T : class
        {
            var result = new JFPagedResult<T>(query)
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };
            result.PageCount = (int)Math.Ceiling((double)result.RowCount / pageSize);
            return result;
        }
    }
}
