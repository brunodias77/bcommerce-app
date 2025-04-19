using bcommerce_server.Domain.Events;

namespace bcommerce_server.Domain.SeedWork;

/// <summary>
/// Representa uma raiz de agregação no modelo de domínio.
/// </summary>
/// <typeparam name="TId">Tipo do identificador único.</typeparam>
public abstract class AggregateRoot<ID> : Entity<ID> where ID : Identifier
{
    protected AggregateRoot(ID id) : base(id)
    {
    }


}