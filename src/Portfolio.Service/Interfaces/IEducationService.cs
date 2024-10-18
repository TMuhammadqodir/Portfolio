using Portfolio.Service.DTOs.Educations;

namespace Portfolio.Service.Interfaces;

public interface IEducationService
{
    Task<EducationResultDto> CreateAsync(EducationCreationDto dto);
    Task<EducationResultDto> UpdateAsync(EducationUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<EducationResultDto> GetByIdAsync(long id);
    Task<IEnumerable<EducationResultDto>> GetAllAsync();
}
