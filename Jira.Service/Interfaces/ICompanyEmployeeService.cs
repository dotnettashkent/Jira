using Jira.Service.DTOs.CompanyEmployees;

namespace Jira.Service.Interfaces
{
    public interface ICompanyEmployeeService
    {
        ValueTask<CompanyEmployeeResultDto> CreateAsync(CompanyEmployeeCreationDto dto);
        ValueTask<CompanyEmployeeResultDto> ModifyAsync(CompanyEmployeeUpdateDto dto);
        ValueTask<bool> DeleteAsync(long id);
        ValueTask<CompanyEmployeeResultDto> GetByIdAsync(long id);
        ValueTask<IEnumerable<CompanyEmployeeResultDto>> GetAllAsync(string search = null);
    }
}
