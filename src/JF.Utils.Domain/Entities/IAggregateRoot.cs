namespace JF.Utils.Domain.Entities
{

    /// <summary>
    /// Aplica esta interfaz marcadora solo a entidades raíz de agregado.
    /// Los repositorios solo trabajarán con entidades raíz de agregado, no con sus hijos.
    /// </summary>
    public interface IAggregateRoot : IEntity
    {
    }

}
