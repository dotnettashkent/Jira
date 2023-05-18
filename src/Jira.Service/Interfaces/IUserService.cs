using Jira.Domain.Entities.Users;
using Jira.Service.DTOs.Users;
using System.Linq.Expressions;

namespace Jira.Service.Interfaces
{
    public interface IUserService
    {
        ValueTask<UserResultDto> CreateAsync(UserCreationDto dto);
        ValueTask<UserResultDto> UpdateAsync(UserUpdateDto dto);
        ValueTask<UserResultDto> ChangePasswordAsync(UserChangePassword dto);
        ValueTask<UserResultDto> GetByIdAsync(long id);
        ValueTask<IEnumerable<UserResultDto>> GetAllAsync(
            Expression<Func<User, bool>> expression = null, string search = null);
        ValueTask<bool> DeleteAsync(long id);
        ValueTask<UserImageResultDto> ImageUploadAsync(UserImageCreationDto dto);
        ValueTask<bool> DeleteUserImageAsync(long userId);
        ValueTask<UserImageResultDto> GetUserImageAsync(long userId);
        ValueTask<UserResultDto> CheckUserAsync(string username, string password = null);
        ValueTask<UserResultDto> UserVerify(string code);
    }
}
