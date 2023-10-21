using FAMS.V0.Shared.Domain.Entities;

namespace FAMS.V0.Shared.Events.TrainingProgramEvents;

public class EventProgramCreated
{
    public TrainingProgram TrainingProgram { get; set; }

    public EventProgramCreated(TrainingProgram trainingProgram)
    {
        TrainingProgram = trainingProgram;
    }
}