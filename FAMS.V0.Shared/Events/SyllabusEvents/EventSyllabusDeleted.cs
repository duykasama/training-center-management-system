namespace FAMS.V0.Shared.Events.SyllabusEvents;

public class EventSyllabusDeleted
{
    public Guid Id { get; set; }

    public EventSyllabusDeleted(Guid id)
    {
        Id = id;
    }
}