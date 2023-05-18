using Jira.Domain.Entities.Positions;
using Jira.Service.DTOs.Positions;
using System.Linq.Expressions;

namespace Jira.Service.Interfaces
{
    public interface IPositionService
    {
        ValueTask<PositionResultDto> CreateAsync(PositionCreationDto dto);
        ValueTask<PositionResultDto> UpdateAsync(PositionUpdateDto dto);
        ValueTask<bool> DeleteAsync(long id);
        ValueTask<PositionResultDto> GetByIdAsync(long id);
        ValueTask<IEnumerable<PositionResultDto>> GetAllAsync(
            Expression<Func<Position, bool>> expression = null, string search = null);
    }
}
