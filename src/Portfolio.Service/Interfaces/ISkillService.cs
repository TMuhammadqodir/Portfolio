using Portfolio.Service.DTOs.Educations;
using Portfolio.Service.DTOs.Skills;

namespace Portfolio.Service.Interfaces;

public interface ISkillService
{
    Task<SkillResultDto> CreateAsync(SkillCreationDto dto);
    Task<SkillResultDto> UpdateAsync(SkillUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<SkillResultDto> GetByIdAsync(long id);
    Task<IEnumerable<SkillResultDto>> GetAllAsync();
    Task<IEnumerable<SkillResultDto>> GetByUserIdAsync(long userId);
}
