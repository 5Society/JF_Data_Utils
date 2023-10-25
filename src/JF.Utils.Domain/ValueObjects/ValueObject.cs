namespace JF.Utils.Domain.ValueObjects
{
    /// <summary>
    /// Clase abstracta que representa un objeto de valor base que implementa la interfaz <see cref="IValueObject"/>.
    /// </summary>
    public abstract class ValueObject : IValueObject
    {

        /// <summary>
        /// Comprueba si dos objetos de valor son iguales.
        /// </summary>
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (left is null ^ right is null)
            {
                return false;
            }

            return left?.Equals(right!) != false;
        }

        /// <summary>
        /// Comprueba si dos objetos de valor no son iguales.
        /// </summary>
        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !(EqualOperator(left, right));
        }

        /// <summary>
        /// Obtiene los componentes de igualdad que representan el objeto de valor.
        /// </summary>
        protected abstract IEnumerable<object> GetEqualityComponents();

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (ValueObject)obj;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }
    }
}
