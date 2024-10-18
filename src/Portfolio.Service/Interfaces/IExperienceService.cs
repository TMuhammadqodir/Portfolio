using Portfolio.Service.DTOs.Experiences;

namespace Portfolio.Service.Interfaces;

public interface IExperienceService
{
    Task<ExperienceResultDto> CreateAsync(ExperienceCreationDto dto);
    Task<ExperienceResultDto> UpdateAsync(ExperienceUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<ExperienceResultDto> GetByIdAsync(long id);
    Task<IEnumerable<ExperienceResultDto>> GetAllAsync();
}
