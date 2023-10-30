namespace JF.Utils.Application.Common
{

    /// <summary>
    /// Interfaz que representa un resultado paginado de entidades de tipo <typeparamref name="IEntity"/>.
    /// </summary>
    /// <typeparam name="IEntity">El tipo de entidad contenida en el resultado paginado.</typeparam>
    public interface IPagedResult<IEntity>
    {
        /// <summary>
        /// Obtiene el número de página actual en el resultado paginado.
        /// </summary>
        int CurrentPage { get; }

        /// <summary>
        /// Obtiene el número total de páginas en el resultado paginado.
        /// </summary>
        int PageCount { get; }

        /// <summary>
        /// Obtiene el tamaño de página utilizado en la paginación.
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// Obtiene el número total de filas en el resultado paginado.
        /// </summary>
        int RowCount { get; }

        /// <summary>
        /// Obtiene la lista de entidades de tipo <typeparamref name="IEntity"/> que forman parte del resultado paginado.
        /// </summary>
        /// <returns>Una lista de entidades paginadas.</returns>
        IList<IEntity> GetResults();
    }

}