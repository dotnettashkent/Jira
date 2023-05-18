using Jira.Service.DTOs.Issues;

namespace Jira.Service.Interfaces
{
    public interface IIssueService
    {
        ValueTask<IssueResultDto> CreateAsync(IssueCreationDto dto);
        ValueTask<IssueResultDto> UpdateAsync(IssueUpdateDto dto);
        ValueTask<bool> DeleteAsync(long id);
        ValueTask<IssueResultDto> GetByIdAsync(long id);
        ValueTask<IEnumerable<IssueResultDto>> GetAllAsync(string search = null);
    }
}
