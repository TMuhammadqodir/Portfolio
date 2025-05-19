using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Portfolio.DataAccess.IRepositories;
using Portfolio.Domain.Entities;
using Portfolio.Service.DTOs.Educations;
using Portfolio.Service.Exceptions;
using Portfolio.Service.Interfaces;

namespace Portfolio.Service.Services;

public class EducationService : IEducationService
{
    private readonly IMapper mapper;
    private readonly IRepository<Education> educationRepository;
    private readonly IRepository<User> userRepository;
    public EducationService(IMapper mapper, 
                            IRepository<Education> educationRepository,
                            IRepository<User> userRepository)
    {
        this.mapper = mapper;
        this.educationRepository = educationRepository;
        this.userRepository = userRepository;
    }

    public async Task<EducationResultDto> CreateAsync(EducationCreationDto dto)
    {
        var existUser = await this.userRepository.GetAsync(u => u.Id == dto.UserId)
            ?? throw new NotFoundException($"This user was not found with {dto.UserId}");
      
        var mappedEducation = this.mapper.Map<Education>(dto);

        await this.educationRepository.AddAsync(mappedEducation);
        await this.educationRepository.SaveAsync();

        return this.mapper.Map<EducationResultDto>(mappedEducation);
    }

    public async Task<EducationResultDto> UpdateAsync(EducationUpdateDto dto)
    {
        var existEducation = await this.educationRepository.GetAsync(e => e.Id.Equals(dto.Id))
            ?? throw new NotFoundException($"This education was not found with {dto.Id}");

        var mappedEducation = this.mapper.Map(dto, existEducation);

        this.educationRepository.Update(mappedEducation);
        await this.educationRepository.SaveAsync();

        return this.mapper.Map<EducationResultDto>(mappedEducation);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existEducation = await this.educationRepository.GetAsync(e => e.Id.Equals(id))
            ?? throw new NotFoundException($"This education was not found with {id}");

        this.educationRepository.Delete(existEducation);
        await this.educationRepository.SaveAsync();

        return true;
    }

    public async Task<EducationResultDto> GetByIdAsync(long id)
    {
        var existEducation = await this.educationRepository.GetAsync(e => e.Id.Equals(id))
            ?? throw new NotFoundException($"This education was not found with {id}");

        return this.mapper.Map<EducationResultDto>(existEducation);
    }

    public async Task<IEnumerable<EducationResultDto>> GetAllAsync()
    {
        var educations = await this.educationRepository.GetAll()
            .ToListAsync();

        return this.mapper.Map<IEnumerable<EducationResultDto>>(educations);
    }

    public async Task<IEnumerable<EducationResultDto>> GetByUserIdAsync(long userId)
    {
        var educations = await this.educationRepository.GetAll(e => e.UserId.Equals(userId))
            .ToListAsync();

        return this.mapper.Map<IEnumerable<EducationResultDto>>(educations);
    }
}
