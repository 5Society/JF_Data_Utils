using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Reflection;


namespace JF.Utils.Infrastructure.Extensions
{
    /// <summary>
    /// Clase estática que proporciona extensiones para facilitar la configuración de filtros de consulta en todas las entidades del modelo de Entity Framework Core.
    /// </summary>
    public static class ModelBuilderExtension
    {
        // Método privado utilizado para obtener información sobre el método SetQueryFilter de forma dinámica.
        private static readonly MethodInfo SetQueryFilterMethod = typeof(ModelBuilderExtension)
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == nameof(SetQueryFilter));

        /// <summary>
        /// Aplica un filtro de consulta en todas las entidades del modelo que implementan la interfaz especificada.
        /// </summary>
        /// <typeparam name="TEntityInterface">Tipo de interfaz que deben implementar las entidades.</typeparam>
        /// <param name="builder">Instancia de ModelBuilder.</param>
        /// <param name="filterExpression">Expresión lambda que representa el filtro de consulta.</param>
        public static void SetQueryFilterOnAllEntities<TEntityInterface>(
            this ModelBuilder builder,
            Expression<Func<TEntityInterface, bool>> filterExpression)
        {
            foreach (var type in builder.Model.GetEntityTypes()
                         .Where(t => t.BaseType == null)
                         .Select(t => t.ClrType)
                         .Where(t => typeof(TEntityInterface).IsAssignableFrom(t)))
                builder.SetEntityQueryFilter(
                    type,
                    filterExpression);
        }

        // Método privado utilizado para invocar dinámicamente el método SetQueryFilter con tipos genéricos.
        private static void SetEntityQueryFilter<TEntityInterface>(
            this ModelBuilder builder,
            Type entityType,
            Expression<Func<TEntityInterface, bool>> filterExpression)
        {
            SetQueryFilterMethod
                .MakeGenericMethod(entityType, typeof(TEntityInterface))
                .Invoke(null, new object[] { builder, filterExpression });
        }

        // Método privado utilizado para aplicar el filtro de consulta en una entidad específica.
        private static void SetQueryFilter<TEntity, TEntityInterface>(
            this ModelBuilder builder,
            Expression<Func<TEntityInterface, bool>> filterExpression)
            where TEntityInterface : class
            where TEntity : class, TEntityInterface
        {
            var concreteExpression = filterExpression
                .Convert<TEntityInterface, TEntity>();
            builder.Entity<TEntity>()
                .AppendQueryFilter(concreteExpression);
        }

        // Método privado utilizado para aplicar el filtro de consulta en una entidad específica.
        // Créditos: Comentario de magiak en GitHub https://github.com/dotnet/efcore/issues/10275#issuecomment-785916356
        private static void AppendQueryFilter<T>(this EntityTypeBuilder entityTypeBuilder,
            Expression<Func<T, bool>> expression)
            where T : class
        {
            var parameterType = Expression.Parameter(entityTypeBuilder.Metadata.ClrType);

            var expressionFilter = ReplacingExpressionVisitor.Replace(
                expression.Parameters.Single(), parameterType, expression.Body);

            if (entityTypeBuilder.Metadata.GetQueryFilter() != null)
            {
                var currentQueryFilter = entityTypeBuilder.Metadata.GetQueryFilter();
                var currentExpressionFilter = ReplacingExpressionVisitor.Replace(
                    currentQueryFilter!.Parameters.Single(), parameterType, currentQueryFilter.Body);
                expressionFilter = Expression.AndAlso(currentExpressionFilter, expressionFilter);
            }

            var lambdaExpression = Expression.Lambda(expressionFilter, parameterType);
            entityTypeBuilder.HasQueryFilter(lambdaExpression);
        }
    }
}
