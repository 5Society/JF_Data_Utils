// Learn more: https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/implement-value-objects

namespace JF.Utils.Domain.ValueObjects
{
    /// <summary>
    /// Interfaz que representa un objeto de valor.
    /// </summary>
    public interface IValueObject
    {
        /// <summary>
        /// Determina si el objeto actual es igual al objeto especificado.
        /// </summary>
        /// <param name="obj">El objeto a comparar.</param>
        /// <returns><c>true</c> si los objetos son iguales; de lo contrario, <c>false</c>.</returns>
        /// <inheritdoc/>
        bool Equals(object? obj);

        /// <summary>
        /// Obtiene el código hash del objeto de valor.
        /// </summary>
        /// <returns>El código hash del objeto de valor.</returns>
        /// <inheritdoc/>
        int GetHashCode();
    }
}