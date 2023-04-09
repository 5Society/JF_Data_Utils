namespace JF.Utils.Application.Common
{

    public interface IPagedResult<TEntity> where TEntity : class
    {
        int CurrentPage { get; }
        int PageCount { get; }
        int PageSize { get; }
        int RowCount { get; }
        IList<TEntity> GetResults();

    }
}