using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Portfolio.DataAccess.IRepositories;
using Portfolio.Domain.Entities;
using Portfolio.Service.DTOs.Experiences;
using Portfolio.Service.Exceptions;
using Portfolio.Service.Interfaces;

namespace Portfolio.Service.Services;

public class ExperienceService : IExperienceService
{
    private readonly IMapper mapper;
    private readonly IRepository<Experience> experienceRepository;
    private readonly IRepository<User> userRepository;
    public ExperienceService(IMapper mapper,
                            IRepository<Experience> experienceRepository,
                            IRepository<User> userRepository)
    {
        this.mapper = mapper;
        this.experienceRepository = experienceRepository;
        this.userRepository = userRepository;
    }

    public async Task<ExperienceResultDto> CreateAsync(ExperienceCreationDto dto)
    {
        var existUser = await this.userRepository.GetAsync(u => u.Id == dto.UserId)
            ?? throw new NotFoundException($"This user was not found with {dto.UserId}");

        var mappedExperience = this.mapper.Map<Experience>(dto);

        await this.experienceRepository.AddAsync(mappedExperience);
        await this.experienceRepository.SaveAsync();

        return this.mapper.Map<ExperienceResultDto>(mappedExperience);
    }

    public async Task<ExperienceResultDto> UpdateAsync(ExperienceUpdateDto dto)
    {
        var existExperience = await this.experienceRepository.GetAsync(e => e.Id.Equals(dto.Id))
            ?? throw new NotFoundException($"This experience was not found with {dto.Id}");

        var mappedExperience = this.mapper.Map(dto, existExperience);

        this.experienceRepository.Update(mappedExperience);
        await this.experienceRepository.SaveAsync();

        return this.mapper.Map<ExperienceResultDto>(mappedExperience);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existExperience = await this.experienceRepository.GetAsync(e => e.Id.Equals(id))
            ?? throw new NotFoundException($"This experience was not found with {id}");

        this.experienceRepository.Delete(existExperience);
        await this.experienceRepository.SaveAsync();

        return true;
    }

    public async Task<ExperienceResultDto> GetByIdAsync(long id)
    {
        var existExperience = await this.experienceRepository.GetAsync(e => e.Id.Equals(id))
            ?? throw new NotFoundException($"This experience was not found with {id}");

        return this.mapper.Map<ExperienceResultDto>(existExperience);
    }

    public async Task<IEnumerable<ExperienceResultDto>> GetAllAsync()
    {
        var experiences = await this.experienceRepository.GetAll()
            .ToListAsync();

        return this.mapper.Map<IEnumerable<ExperienceResultDto>>(experiences);
    }
}
