using AutoMapper;
using Portfolio.DataAccess.IRepositories;
using Portfolio.Domain.Entities;
using Portfolio.Service.DTOs.Skills;
using Portfolio.Service.Exceptions;
using Portfolio.Service.Interfaces;

namespace Portfolio.Service.Services;

public class SkillService : ISkillService
{
    private readonly IMapper mapper;
    private readonly IRepository<Skill> skillRepository;
    private readonly IRepository<User> userRepository;
    public SkillService(IMapper mapper,
                            IRepository<Skill> skillRepository,
                            IRepository<User> userRepository)
    {
        this.mapper = mapper;
        this.skillRepository = skillRepository;
        this.userRepository = userRepository;
    }

    public async Task<SkillResultDto> CreateAsync(SkillCreationDto dto)
    {
        var existUser = await this.userRepository.GetAsync(u => u.Id == dto.UserId)
            ?? throw new NotFoundException($"This user was not found with {dto.UserId}");

        var mappedSkill = this.mapper.Map<Skill>(dto);

        await this.skillRepository.AddAsync(mappedSkill);
        await this.skillRepository.SaveAsync();

        return this.mapper.Map<SkillResultDto>(mappedSkill);
    }

    public async Task<SkillResultDto> UpdateAsync(SkillUpdateDto dto)
    {
        var existSkill = await this.skillRepository.GetAsync(s => s.Id.Equals(dto.Id))
            ?? throw new NotFoundException($"This skill was not found with {dto.Id}");

        var mappedSkill = this.mapper.Map(dto, existSkill);

        this.skillRepository.Update(mappedSkill);
        await this.skillRepository.SaveAsync();

        return this.mapper.Map<SkillResultDto>(mappedSkill);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existSkill = await this.skillRepository.GetAsync(s => s.Id.Equals(id))
            ?? throw new NotFoundException($"This skill was not found with {id}");

        this.skillRepository.Delete(existSkill);
        await this.skillRepository.SaveAsync();

        return true;
    }

    public async Task<SkillResultDto> GetByIdAsync(long id)
    {
        var existSkill = await this.skillRepository.GetAsync(s => s.Id.Equals(id))
            ?? throw new NotFoundException($"This skill was not found with {id}");

        return this.mapper.Map<SkillResultDto>(existSkill);
    }

    public async Task<IEnumerable<SkillResultDto>> GetAllAsync()
    {
        var skills = await this.skillRepository.GetAll()
            .ToListAsync();

        return this.mapper.Map<IEnumerable<SkillResultDto>>(skills);
    }
}
