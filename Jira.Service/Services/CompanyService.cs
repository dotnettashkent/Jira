using AutoMapper;
using Jira.DAL.IRepository;
using Jira.Domain.Entities.Companies;
using Jira.Domain.Entities.Users;
using Jira.Service.DTOs.Companies;
using Jira.Service.DTOs.Users;
using Jira.Service.Exceptions;
using Jira.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Jira.Service.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IMapper mapper;
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Company> companyRepository;
        public CompanyService(
            IMapper mapper,
            IRepository<User> userRepository,
            IRepository<Company> companyRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.companyRepository = companyRepository;
        }

        public async ValueTask<CompanyResultDto> CreateAsync(CompanyCreationDto dto)
        {
            Company company = await this.companyRepository
                .SelectAsync(c => c.Name.ToLower() == dto.Name.ToLower() && !c.IsDeleted);
            if (company is not null)
                throw new JiraException(409, "Company already exist for given argument");

            var user = await this.userRepository
                .SelectAsync(t => t.Id.Equals(dto.OwnerId) && !t.IsDeleted);
            if (user is null)
                throw new JiraException(404, "User is not found");

            Company mappedCompany = this.mapper.Map<Company>(dto);
            Company createdCompany = await this.companyRepository.InsertAsync(mappedCompany);
            await this.companyRepository.SaveChangesAsync();

            var result = this.mapper.Map<CompanyResultDto>(createdCompany);
            result.Owner = this.mapper.Map<UserResultDto>(user);
            return result;
        }

        public async ValueTask<bool> DeleteAsync(long companyId)
        {
            Company company = await this.companyRepository
                .SelectAsync(company => company.Id.Equals(companyId) && !company.IsDeleted);
            if (company is null)
                throw new JiraException(404, "Company is not found for given id");

            bool result = await this.companyRepository.DeleteAsync(company);
            await this.companyRepository.SaveChangesAsync();
            return result;
        }

        public async ValueTask<List<CompanyResultDto>> GetAllAsync(string search = null)
        {
            IQueryable<Company> companies = companyRepository
                .SelectAll(t => !t.IsDeleted, isTracking: false)
                .Include(t => t.Owner);

            var result =
                mapper.Map<List<CompanyResultDto>>(companies);

            if (!string.IsNullOrEmpty(search))
                return result.Where(c =>
                    c.Name.ToLower().Contains(search.ToLower())).ToList();
            return result;
        }

        public async ValueTask<CompanyResultDto> GetByIdAsync(long companyId)
        {
            Company company = await companyRepository
                .SelectAsync(company => company.Id.Equals(companyId) && !company.IsDeleted);
            if (company is null)
                throw new JiraException(404, "Company not found for given id");

            var user = await this.userRepository
                .SelectAsync(t => t.Id.Equals(company.OwnerId) && !t.IsDeleted);
            if (user is null)
                throw new JiraException(404, "User is not found");

            var result = this.mapper.Map<CompanyResultDto>(company);
            result.Owner = this.mapper.Map<UserResultDto>(user);
            return result;
        }

        public async ValueTask<CompanyResultDto> ModifyAsync(CompanyUpdateDto dto)
        {
            Company company = await companyRepository.SelectAsync(u => u.Id.Equals(dto.Id) && !u.IsDeleted);
            if (company is null)
                throw new JiraException(404, "Company not found");

            var user = await this.userRepository
                .SelectAsync(t => t.Id.Equals(company.OwnerId) && !t.IsDeleted);
            if (user is null)
                throw new JiraException(404, "User is not found");
            this.mapper.Map(dto, company);
            company.UpdatedAt = DateTime.UtcNow;
            await companyRepository.SaveChangesAsync();

            var result = this.mapper.Map<CompanyResultDto>(company);
            result.Owner = this.mapper.Map<UserResultDto>(user);
            return result;
        }
    }
}
