
using JF.Utils.Application.Common;

namespace JF.Utils.Infrastructure.Common
{
    /// <summary>
    /// Implementación de la interfaz <see cref="IPagedResult{TEntity}"/> que representa un resultado paginado de una consulta IQueryable.
    /// </summary>
    /// <typeparam name="TEntity">Tipo de entidad en la consulta.</typeparam>
    public class JFPagedResult<TEntity> : IPagedResult<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Obtiene o establece la consulta IQueryable asociada al resultado paginado.
        /// </summary>
        public IQueryable<TEntity> Query { get; internal set; }

        /// <summary>
        /// Obtiene o establece el número de la página actual.
        /// </summary>
        public int CurrentPage { get; internal set; }

        /// <summary>
        /// Obtiene o establece el número total de páginas en el conjunto de resultados paginados.
        /// </summary>
        public int PageCount { get; internal set; }

        /// <summary>
        /// Obtiene o establece el tamaño de la página (número de elementos por página).
        /// </summary>
        public int PageSize { get; internal set; }

        /// <summary>
        /// Obtiene o establece el número total de filas en el conjunto de resultados no paginados.
        /// </summary>
        public int RowCount { get; internal set; }

        /// <summary>
        /// Obtiene una lista de entidades correspondientes a la página actual y tamaño de página especificados.
        /// </summary>
        /// <returns>Lista de entidades correspondientes a la página actual y tamaño de página especificados.</returns>
        public IList<TEntity> GetResults()
        {
            return Query.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="JFPagedResult{TEntity}"/> con la consulta IQueryable especificada.
        /// </summary>
        /// <param name="query">Consulta IQueryable asociada al resultado paginado.</param>
        public JFPagedResult(IQueryable<TEntity> query)
        {
            Query = query;
        }
    }

}