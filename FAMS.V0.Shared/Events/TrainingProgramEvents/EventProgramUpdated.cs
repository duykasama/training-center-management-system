using FAMS.V0.Shared.Domain.Entities;

namespace FAMS.V0.Shared.Events.TrainingProgramEvents;

public class EventProgramUpdated
{
    public TrainingProgram TrainingProgram{ get; set; }

    public EventProgramUpdated(TrainingProgram trainingProgram)
    {
        TrainingProgram = trainingProgram;
    }
}