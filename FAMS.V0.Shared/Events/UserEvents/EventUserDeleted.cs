namespace FAMS.V0.Shared.Events.UserEvents;

public class EventUserDeleted
{
    public Guid Id { get; set; }

    public EventUserDeleted(Guid id)
    {
        Id = id;
    }
}