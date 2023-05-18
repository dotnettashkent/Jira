using AutoMapper;
using Jira.DAL.IRepository;
using Jira.Domain.Entities.Companies;
using Jira.Domain.Entities.Issues;
using Jira.Service.DTOs.Companies;
using Jira.Service.DTOs.Issues;
using Jira.Service.Exceptions;
using Jira.Service.Interfaces;
using System.Linq.Expressions;

namespace Jira.Service.Services
{
    public class IssueCategoryService : IIssueCategoryService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Company> companyRepository;
        private readonly IRepository<IssueCategory> IssueCategoryRepository;
        public IssueCategoryService(
            IMapper mapper,
            IRepository<IssueCategory> repository,
            IRepository<Company> companyRepository)
        {
            this.mapper = mapper;
            this.IssueCategoryRepository = repository;
            this.companyRepository = companyRepository;
        }
        public async ValueTask<IssueCategoryResultDto> CreateAsync(IssueCategoryCreationDto dto)
        {
            IssueCategory issueCategory = await this.IssueCategoryRepository
            .SelectAsync(u => u.Name.ToLower() == dto.Name.ToLower() && u.CompanyId == dto.CompanyId && !u.IsDeleted);
            if (issueCategory is not null)
                throw new JiraException(403, "IssueCategory already exist");

            var company = await this.companyRepository
                .SelectAsync(t => t.Id.Equals(dto.CompanyId) && !t.IsDeleted);
            if (company is null)
                throw new JiraException(404, "Company is not found");

            var mappedIssueCategory = this.mapper.Map<IssueCategory>(dto);
            var createdIssueCategory = await this.IssueCategoryRepository.InsertAsync(mappedIssueCategory);
            await this.IssueCategoryRepository.SaveChangesAsync();

            var result = this.mapper.Map<IssueCategoryResultDto>(createdIssueCategory);
            result.Company = this.mapper.Map<CompanyResultDto>(company);
            return result;
        }

        public async ValueTask<bool> DeleteAsync(long id)
        {
            var issueCategory = await this.IssueCategoryRepository
            .SelectAsync(issueCategory => issueCategory.Id == id);
            if (issueCategory is null)
                throw new JiraException(404, "IssueCategory not found");

            await this.IssueCategoryRepository.DeleteAsync(issueCategory);
            await this.IssueCategoryRepository.SaveChangesAsync();
            return true;
        }

        public async ValueTask<IEnumerable<IssueCategoryResultDto>> GetAllAsync(
            Expression<Func<IssueCategory,
            bool>> expression = null,
            string search = null)
        {
            var issueCategories = this.IssueCategoryRepository
            .SelectAll(expression, new string[] { "Company" }, isTracking: false);

            var result = this.mapper.Map<IEnumerable<IssueCategoryResultDto>>(issueCategories);
            if (!string.IsNullOrEmpty(search))
                return result
                    .Where(c => c.Name.ToLower() == search).ToList();

            return result;
        }

        public async ValueTask<IssueCategoryResultDto> GetByIdAsync(long id)
        {
            var issueCategory = await IssueCategoryRepository
            .SelectAsync(issueCategory => issueCategory.Id.Equals(id));
            if (issueCategory is null)
                throw new JiraException(404, "IssueCategory not found");
            return mapper.Map<IssueCategoryResultDto>(issueCategory);
        }

        public async ValueTask<IssueCategoryResultDto> UpdateAsync(IssueCategoryUpdateDto dto)
        {
            var updatingIssueCategory = await IssueCategoryRepository
            .SelectAsync(issueCategory => issueCategory.Id.Equals(dto.Id));
            if (updatingIssueCategory is null)
                throw new JiraException(404, "IssueCategory not found");

            this.mapper.Map(dto, updatingIssueCategory);
            updatingIssueCategory.UpdatedAt = DateTime.UtcNow;
            await IssueCategoryRepository.SaveChangesAsync();
            return mapper.Map<IssueCategoryResultDto>(updatingIssueCategory);
        }
    }
}
