

using JF.Utils.Application.Common;
using JF.Utils.Infrastructure.Common;

namespace JF.Utils.Infrastructure.Extensions
{

    /// <summary>
    /// Clase estática que proporciona extensiones para realizar consultas paginadas en colecciones IQueryable.
    /// </summary>
    public static class PagedQueryExtension
    {
        /// <summary>
        /// Realiza una consulta paginada en una colección IQueryable.
        /// </summary>
        /// <typeparam name="T">Tipo de elemento en la colección IQueryable.</typeparam>
        /// <param name="query">La consulta IQueryable.</param>
        /// <param name="page">Número de la página que se desea recuperar.</param>
        /// <param name="pageSize">Tamaño de la página (número de elementos por página).</param>
        /// <returns>Resultado paginado que contiene la página actual, el tamaño de la página, el número total de filas y la colección de elementos.</returns>
        /// <remarks>El tipo T debe ser una clase.</remarks>
        public static IPagedResult<T> GetPaged<T>(this IQueryable<T> query, int page, int pageSize)
            where T : class
        {
            // Crea un objeto de resultado paginado utilizando la implementación JFPagedResult<T>.
            var result = new JFPagedResult<T>(query)
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            // Calcula el número total de páginas en base al tamaño de la página y el número total de filas.
            result.PageCount = (int)Math.Ceiling((double)result.RowCount / pageSize);

            // Devuelve el resultado paginado.
            return result;
        }
    }

}
