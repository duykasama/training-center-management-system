using FAMS.V0.Services.SyllabusService.Dtos;
using FAMS.V0.Shared.Domain.Entities;

namespace FAMS.V0.Services.SyllabusService.Mapper;

public static class SyllabusMapper
{
    public static Syllabus DtoSyllabusCreateToEntity(DtoSyllabusCreate dto)
    {
        return new Syllabus()
        {
            TopicCode = dto.TopicCode,
            TopicName = dto.TopicName,
            Version = dto.Version,
            TechnicalGroup = dto.TechnicalGroup,
            TrainingAudience = dto.TrainingAudience,
            TopicOutline = dto.TopicOutline,
            TrainingMaterials = dto.TrainingMaterials,
            TrainingPrinciples = dto.TrainingPrinciples,
            Priority = dto.Priority,
            PublishStatus = dto.PublishStatus,
            CreatedBy = dto.CreatedBy,
            CreatedDate = DateTime.UtcNow
        };
    }

    public static Syllabus DtoSyllabusUpdateToEntity(DtoSyllabusUpdate dto)
    {
        return new Syllabus()
        {
            TopicCode = dto.TopicCode,
            TopicName = dto.TopicName,
            Version = dto.Version,
            TechnicalGroup = dto.TechnicalGroup,
            TrainingAudience = dto.TrainingAudience,
            TopicOutline = dto.TopicOutline,
            TrainingMaterials = dto.TrainingMaterials,
            TrainingPrinciples = dto.TrainingPrinciples,
            Priority = dto.Priority,
            PublishStatus = dto.PublishStatus,
            ModifiedBy = dto.ModifiedBy,
            ModifiedDate = DateTime.UtcNow
        };
    }
}