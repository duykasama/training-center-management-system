using FAMS.V0.Services.SyllabusService.Entities;

namespace FAMS.V0.Shared.Events.SyllabusEvents;

public class EventSyllabusCreated
{
    public Syllabus Syllabus { get; set; }

    public EventSyllabusCreated(Syllabus syllabus)
    {
        Syllabus = syllabus;
    }
}