using AutoMapper;
using Jira.DAL.IRepository;
using Jira.Domain.Entities.Positions;
using Jira.Service.DTOs.Positions;
using Jira.Service.Exceptions;
using Jira.Service.Interfaces;
using System.Linq.Expressions;

namespace Jira.Service.Services
{
    public class PositionService : IPositionService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Position> positionRepository;
        public PositionService(IRepository<Position> repository, IMapper mapper)
        {
            this.mapper = mapper;
            this.positionRepository = repository;
        }
        public async ValueTask<PositionResultDto> CreateAsync(PositionCreationDto dto)
        {
            Position position = await this.positionRepository
            .SelectAsync(u => u.Name.ToLower() == dto.Name.ToLower());
            if (position is not null)
                throw new JiraException(403, "Position already exist with this name");

            var mappedPosition = mapper.Map<Position>(dto);
            var result = await this.positionRepository.InsertAsync(mappedPosition);
            await this.positionRepository.SaveChangesAsync();
            return this.mapper.Map<PositionResultDto>(result);
        }

        public async ValueTask<bool> DeleteAsync(long id)
        {
            var position = await this.positionRepository.SelectAsync(position => position.Id.Equals(id));
            if (position is null)
                throw new JiraException(404, "Position not found");

            await this.positionRepository.DeleteAsync(position);
            await this.positionRepository.SaveChangesAsync();
            return true;
        }

        public async ValueTask<IEnumerable<PositionResultDto>> GetAllAsync(Expression<Func<Position, bool>> expression = null, string search = null)
        {
            var positions = positionRepository.SelectAll(expression, isTracking: false);
            var result = mapper.Map<IEnumerable<PositionResultDto>>(positions);
            if (!string.IsNullOrEmpty(search))
                return result
                    .Where(u => u.Name.ToLower().Contains(search.ToLower())).ToList();
            return result;
        }

        public async ValueTask<PositionResultDto> GetByIdAsync(long id)
        {
            Position position = await this.positionRepository.SelectAsync(position => position.Id.Equals(id));
            if (position is null)
                throw new JiraException(404, "Position not found");
            return mapper.Map<PositionResultDto>(position);
        }

        public async ValueTask<PositionResultDto> UpdateAsync(PositionUpdateDto dto)
        {
            Position updatingPosition = await this.positionRepository.SelectAsync(p => p.Id.Equals(dto.Id));
            if (updatingPosition is null)
                throw new JiraException(404, "Position not found");

            this.mapper.Map(dto, updatingPosition);
            updatingPosition.UpdatedAt = DateTime.UtcNow;
            await this.positionRepository.SaveChangesAsync();
            return mapper.Map<PositionResultDto>(updatingPosition);
        }
    }
}
