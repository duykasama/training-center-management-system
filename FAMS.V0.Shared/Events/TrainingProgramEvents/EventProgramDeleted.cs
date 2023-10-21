namespace FAMS.V0.Shared.Events.TrainingProgramEvents;

public class EventProgramDeleted
{
    public Guid Id { get; set; }

    public EventProgramDeleted(Guid id)
    {
        Id = id;
    }
}