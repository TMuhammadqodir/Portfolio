using AutoMapper;
using Portfolio.DataAccess.IRepositories;
using Portfolio.Domain.Entities;
using Portfolio.Service.DTOs.Projects;
using Portfolio.Service.Exceptions;
using Portfolio.Service.Interfaces;

namespace Portfolio.Service.Services;

public class ProjectService : IProjectService
{
    private readonly IMapper mapper;
    private readonly IRepository<Project> projectRepository;
    private readonly IRepository<User> userRepository;
    public ProjectService(IMapper mapper,
                            IRepository<Project> projectRepository,
                            IRepository<User> userRepository)
    {
        this.mapper = mapper;
        this.projectRepository = projectRepository;
        this.userRepository = userRepository;
    }

    public async Task<ProjectResultDto> CreateAsync(ProjectCreationDto dto)
    {
        var existUser = await this.userRepository.GetAsync(u => u.Id == dto.UserId)
            ?? throw new NotFoundException($"This user was not found with {dto.UserId}");

        var mappedProject = this.mapper.Map<Project>(dto);

        await this.projectRepository.AddAsync(mappedProject);
        await this.projectRepository.SaveAsync();

        return this.mapper.Map<ProjectResultDto>(mappedProject);
    }

    public async Task<ProjectResultDto> UpdateAsync(ProjectUpdateDto dto)
    {
        var existProject = await this.projectRepository.GetAsync(e => e.Id.Equals(dto.Id))
            ?? throw new NotFoundException($"This project was not found with {dto.Id}");

        var mappedProject = this.mapper.Map(dto, existProject);

        this.projectRepository.Update(mappedProject);
        await this.projectRepository.SaveAsync();

        return this.mapper.Map<ProjectResultDto>(mappedProject);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existProject = await this.projectRepository.GetAsync(e => e.Id.Equals(id))
            ?? throw new NotFoundException($"This project was not found with {id}");

        this.projectRepository.Delete(existProject);
        await this.projectRepository.SaveAsync();

        return true;
    }

    public async Task<ProjectResultDto> GetByIdAsync(long id)
    {
        var existProject = await this.projectRepository.GetAsync(e => e.Id.Equals(id))
            ?? throw new NotFoundException($"This project was not found with {id}");

        return this.mapper.Map<ProjectResultDto>(existProject);
    }

    public async Task<IEnumerable<ProjectResultDto>> GetAllAsync()
    {
        var projects = await this.projectRepository.GetAll()
            .ToListAsync();

        return this.mapper.Map<IEnumerable<ProjectResultDto>>(projects);
    }
}
