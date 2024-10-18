using AutoMapper;
using Portfolio.Domain.Entities;
using Portfolio.Service.DTOs.Assets;
using Portfolio.Service.DTOs.Educations;
using Portfolio.Service.DTOs.Experiences;
using Portfolio.Service.DTOs.Projects;
using Portfolio.Service.DTOs.Skills;
using Portfolio.Service.DTOs.Users;

namespace Portfolio.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Asset, AssetCreationDto>().ReverseMap();
        CreateMap<Asset, AssetResultDto>().ReverseMap();

        CreateMap<Education, EducationCreationDto>().ReverseMap();
        CreateMap<Education, EducationUpdateDto>().ReverseMap();
        CreateMap<Education, EducationResultDto>().ReverseMap();

        CreateMap<Experience, ExperienceCreationDto>().ReverseMap();
        CreateMap<Experience, ExperienceUpdateDto>().ReverseMap();
        CreateMap<Experience, ExperienceResultDto>().ReverseMap();

        CreateMap<Project, ProjectCreationDto>().ReverseMap();
        CreateMap<Project, ProjectUpdateDto>().ReverseMap();
        CreateMap<Project, ProjectResultDto>().ReverseMap();

        CreateMap<Skill, SkillCreationDto>().ReverseMap();
        CreateMap<Skill, SkillUpdateDto>().ReverseMap();
        CreateMap<Skill, SkillResultDto>().ReverseMap();

        CreateMap<User, UserCreationDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
        CreateMap<User, UserResultDto>().ReverseMap();
    }
}
