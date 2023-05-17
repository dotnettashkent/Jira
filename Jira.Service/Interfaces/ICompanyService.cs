using Jira.Service.DTOs.Companies;

namespace Jira.Service.Interfaces
{
    public interface ICompanyService
    {
        ValueTask<CompanyResultDto> CreateAsync(CompanyCreationDto dto);
        ValueTask<CompanyResultDto> ModifyAsync(CompanyUpdateDto dto);
        ValueTask<bool> DeleteAsync(long companyId);
        ValueTask<CompanyResultDto> GetByIdAsync(long companyId);
        ValueTask<List<CompanyResultDto>> GetAllAsync(string search = null);
    }
}
