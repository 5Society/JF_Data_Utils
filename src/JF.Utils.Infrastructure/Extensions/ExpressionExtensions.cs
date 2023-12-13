using System.Collections.ObjectModel;
using System.Linq.Expressions;


namespace JF.Utils.Infrastructure.Extensions
{
    /// <summary>
    /// Clase estática que proporciona extensiones para la conversión de expresiones lambda entre tipos relacionados.
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Convierte una expresión lambda de un tipo fuente a un tipo destino, manteniendo la lógica de la expresión.
        /// </summary>
        /// <typeparam name="TSource">Tipo de origen en la expresión lambda.</typeparam>
        /// <typeparam name="TTarget">Tipo de destino al que se va a convertir la expresión lambda.</typeparam>
        /// <param name="root">Expresión lambda original.</param>
        /// <returns>Expresión lambda convertida al tipo destino.</returns>
        /// <remarks>
        /// Esta funcionalidad se basa en una solución proporcionada en el siguiente post de StackOverflow:
        /// https://stackoverflow.com/questions/38316519/replace-parameter-type-in-lambda-expression
        /// </remarks>
        public static Expression<Func<TTarget, bool>> Convert<TSource, TTarget>(
            this Expression<Func<TSource, bool>> root)
        {
            // Crea una instancia del visitante que realizará la conversión de tipo de parámetros.
            var visitor = new ParameterTypeVisitor<TSource, TTarget>();
            // Utiliza el visitante para realizar la conversión y devuelve la nueva expresión lambda.
            return (Expression<Func<TTarget, bool>>)visitor.Visit(root);
        }

        /// <summary>
        /// Clase privada que implementa un visitante de expresiones para cambiar el tipo de parámetros en la expresión lambda.
        /// </summary>
        private sealed class ParameterTypeVisitor<TSource, TTarget> : ExpressionVisitor
        {
            private ReadOnlyCollection<ParameterExpression>? _parameters;

            /// <summary>
            /// Realiza la visita y la conversión del tipo de un parámetro en la expresión lambda.
            /// </summary>
            /// <param name="node">Parámetro de la expresión lambda.</param>
            /// <returns>Expresión lambda con el tipo de parámetro cambiado si es necesario.</returns>
            protected override Expression VisitParameter(ParameterExpression node)
            {
                return _parameters?.FirstOrDefault(p => p.Name == node.Name)
                       ?? (node.Type == typeof(TSource) ? Expression.Parameter(typeof(TTarget), node.Name) : node);
            }

            /// <summary>
            /// Realiza la visita y la conversión del tipo de los parámetros en la expresión lambda.
            /// </summary>
            /// <typeparam name="T">Tipo de la expresión lambda.</typeparam>
            /// <param name="node">Expresión lambda.</param>
            /// <returns>Expresión lambda con el tipo de parámetros cambiado si es necesario.</returns>
            protected override Expression VisitLambda<T>(Expression<T> node)
            {
                _parameters = VisitAndConvert(node.Parameters, "VisitLambda");
                return Expression.Lambda(Visit(node.Body), _parameters);
            }
        }
    }

}
