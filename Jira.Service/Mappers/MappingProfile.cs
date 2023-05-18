using AutoMapper;
using Jira.Domain.Entities.Companies;
using Jira.Domain.Entities.Issues;
using Jira.Domain.Entities.Positions;
using Jira.Domain.Entities.Users;
using Jira.Service.DTOs.Companies;
using Jira.Service.DTOs.CompanyEmployees;
using Jira.Service.DTOs.Issues;
using Jira.Service.DTOs.Positions;
using Jira.Service.DTOs.Users;

namespace Jira.Service.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<User, UserCreationDto>().ReverseMap();
            CreateMap<User, UserResultDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
            CreateMap<UserCreationDto, UserUpdateDto>().ReverseMap();
            CreateMap<UserImage, UserImageResultDto>().ReverseMap();

            // Position
            CreateMap<Position, PositionCreationDto>().ReverseMap();
            CreateMap<Position, PositionResultDto>().ReverseMap();

            // Company
            CreateMap<Company, CompanyCreationDto>().ReverseMap();
            CreateMap<Company, CompanyResultDto>().ReverseMap();
            CreateMap<Company, CompanyUpdateDto>().ReverseMap();

            // IssueCategory
            CreateMap<IssueCategory, IssueCategoryCreationDto>().ReverseMap();
            CreateMap<IssueCategory, IssueCategoryResultDto>().ReverseMap();

            // Issue
            CreateMap<Issue, IssueCreationDto>().ReverseMap();
            CreateMap<Issue, IssueResultDto>().ReverseMap();
            CreateMap<Issue, IssueUpdateDto>().ReverseMap();
            CreateMap<Issue, IssueEmployeeDto>().ReverseMap();

            // Employee
            CreateMap<CompanyEmployee, CompanyEmployeeCreationDto>().ReverseMap();
            CreateMap<CompanyEmployee, CompanyEmployeeResultDto>().ReverseMap();
            CreateMap<CompanyEmployee, CompanyEmployeeUpdateDto>().ReverseMap();
        }
    }
}
