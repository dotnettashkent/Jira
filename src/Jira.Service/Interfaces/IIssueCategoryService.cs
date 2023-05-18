using Jira.Domain.Entities.Issues;
using Jira.Service.DTOs.Issues;
using System.Linq.Expressions;

namespace Jira.Service.Interfaces
{
    public interface IIssueCategoryService
    {
        ValueTask<IssueCategoryResultDto> CreateAsync(IssueCategoryCreationDto dto);
        ValueTask<IssueCategoryResultDto> UpdateAsync(IssueCategoryUpdateDto dto);
        ValueTask<bool> DeleteAsync(long id);
        ValueTask<IssueCategoryResultDto> GetByIdAsync(long id);
        ValueTask<IEnumerable<IssueCategoryResultDto>> GetAllAsync(
            Expression<Func<IssueCategory, bool>> expression = null, string search = null);
    }
}
