namespace bcommerce_server.Domain.Events;

public abstract class DomainEvent
{
    public DateTime OccuredOn { get; set; }

    protected DomainEvent()
    {
        OccuredOn = DateTime.Now;
    }
}