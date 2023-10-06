namespace FAMS.V0.Shared.Events.UserEvents;

public class UserDeletedEvent
{
    public Guid Id { get; set; }

    public UserDeletedEvent(Guid id)
    {
        Id = id;
    }
}