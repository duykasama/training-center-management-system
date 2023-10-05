using FAMS.V0.Shared.Interfaces;

namespace FAMS.V0.Services.SyllabusService.Entities;

public class Syllabus : IEntity
{
    public Guid Id { get; set; }
    public string TopicCode { get; set; }
    public string TopicName { get; set; }
    public string TechnicalGroup { get; set; }
    public string Version { get; set; }
    public string TrainingAudience { get; set; }
    public string TopicOutline { get; set; }
    public string TrainingMaterials { get; set; }
    public string TrainingPrinciples { get; set; }
    public int Priority { get; set; }
    public string PublishStatus { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
}