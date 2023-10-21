using FAMS.V0.Services.ProgramService.Dtos;
using FAMS.V0.Shared.Domain.Entities;

namespace FAMS.V0.Services.ProgramService.Mapper;

public class TrainingProgramMapper
{
    public static TrainingProgramDto EntityToProgramDto(TrainingProgram programEntity)
    {
        return new TrainingProgramDto()
        {
            Id = programEntity.Id,
            TrainingProgramCode = programEntity.TrainingProgramCode,
            Name = programEntity.Name,
            CreatedBy = programEntity.CreatedBy,
            CreatedDate = programEntity.CreatedDate,
            Duration = programEntity.Duration,
            ModifiedDate = programEntity.ModifiedDate,
            StartTime = programEntity.StartTime,
            Status = programEntity.Status
        };

    }

    public static TrainingProgram ProgramDtoToEntity(TrainingProgramDto programDto)
    {
        return new TrainingProgram()
        {
            Id = programDto.Id,
            TrainingProgramCode = programDto.TrainingProgramCode,
            Name = programDto.Name,
            CreatedBy = programDto.CreatedBy,
            CreatedDate = programDto.CreatedDate,
            Duration = programDto.Duration,
            ModifiedDate = programDto.ModifiedDate,
            StartTime = programDto.StartTime,
            Status = programDto.Status
        };
    }

    public static TrainingProgram CreateTrainingProgramDtoToEntity(CreateTrainingProgramDto programDto)
    {
        return new TrainingProgram()
        {
            TrainingProgramCode = programDto.TrainingProgramCode,
            Name = programDto.Name,
            CreatedBy = programDto.CreatedBy,
            CreatedDate = programDto.CreatedDate,
            Duration = programDto.Duration,
            ModifiedDate = programDto.ModifiedDate,
            StartTime = programDto.StartTime,
            Status = programDto.Status
        };
    }

    public static TrainingProgram UpdateTrainingProgramDtoToEntity(TrainingProgram program, TrainingProgramDto programDto)
    {
        program.TrainingProgramCode = programDto.TrainingProgramCode;
        program.Name = programDto.Name;
        program.CreatedBy = programDto.CreatedBy;
        program.CreatedDate = programDto.CreatedDate;
        program.Duration = programDto.Duration;
        program.ModifiedDate = programDto.ModifiedDate;
        program.StartTime = programDto.StartTime;
        program.Status = programDto.Status;
        return program;
    }

}
