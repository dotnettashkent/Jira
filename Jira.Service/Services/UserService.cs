using AutoMapper;
using Jira.DAL.IRepository;
using Jira.Domain.Entities.Users;
using Jira.Service.DTOs.Users;
using Jira.Service.Exceptions;
using Jira.Service.Extensions;
using Jira.Service.Helpers;
using Jira.Service.Interfaces;
using System.Linq.Expressions;

namespace Jira.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly IRepository<User> userRepository;
        private readonly IRepository<UserImage> userImageRepository;
        public UserService(
            IMapper mapper,
            IRepository<User> repository,
            IRepository<UserImage> userImageRepository)
        {
            this.mapper = mapper;
            this.userRepository = repository;
            this.userImageRepository = userImageRepository;
        }
        public async ValueTask<UserResultDto> ChangePasswordAsync(UserChangePassword dto)
        {
            User existUser = await userRepository.SelectAsync(u => u.Username == dto.Username);
            if (existUser is null)
                throw new Exception("This username is not exist");
            else if (dto.NewPassword != dto.ComfirmPassword)
                throw new Exception("New password and confirm password are not equal");
            else if (existUser.Password != dto.OldPassword)
                throw new Exception("Password is incorrect");

            existUser.Password = dto.ComfirmPassword;
            await userRepository.SaveChangesAsync();
            return mapper.Map<UserResultDto>(existUser);
        }

        public async ValueTask<UserResultDto> CheckUserAsync(string username, string password = null)
        {
            var user = await this.userRepository.SelectAsync(t => t.Username.Equals(username));
            if (user is null)
                throw new JiraException(404, "User is not found");
            return this.mapper.Map<UserResultDto>(user);
        }

        public async ValueTask<UserResultDto> CreateAsync(UserCreationDto dto)
        {
            User user = await this.userRepository.SelectAsync(u => u.Username.ToLower() == dto.Username.ToLower());
            if (user is not null)
                throw new JiraException(403, "User already exist with this username");

            User mappedUser = mapper.Map<User>(dto);
            var result = await this.userRepository.InsertAsync(mappedUser);
            await this.userRepository.SaveChangesAsync();
            return this.mapper.Map<UserResultDto>(result);
        }

        public async ValueTask<bool> DeleteAsync(long id)
        {
            var user = await this.userRepository.SelectAsync(u => u.Id.Equals(id));
            if (user is null)
                throw new JiraException(404, "User not found");

            await this.userRepository.DeleteAsync(user);
            await this.userRepository.SaveChangesAsync();
            return true;
        }

        public async ValueTask<bool> DeleteUserImageAsync(long userId)
        {
            var userImage = await this.userImageRepository.SelectAsync(t => t.UserId.Equals(userId));
            if (userImage is null)
                throw new JiraException(404, "Image is not found");

            File.Delete(userImage.Path);
            await this.userImageRepository.DeleteAsync(userImage);
            await this.userImageRepository.SaveChangesAsync();
            return true;
        }

        public async ValueTask<IEnumerable<UserResultDto>> GetAllAsync(
            Expression<Func<User, bool>> expression = null, string search = null)
        {
            var users = userRepository.SelectAll(expression, isTracking: false);
            var result = mapper.Map<IEnumerable<UserResultDto>>(users);

            foreach (var item in result)
                item.Image = mapper.Map<UserImageResultDto>(
                    await this.userImageRepository.SelectAsync(t => t.UserId.Equals(item.Id)));

            if (!string.IsNullOrEmpty(search))
                return result.Where(
                    u => u.Firstname.ToLower().Contains(search.ToLower()) ||
                    u.Lastname.ToLower().Contains(search.ToLower()) ||
                    u.Username.ToLower().Contains(search.ToLower())).ToList();

            return result;
        }

        public async ValueTask<UserResultDto> GetByIdAsync(long id)
        {
            var user = await userRepository.SelectAsync(u => u.Id.Equals(id));
            if (user is null)
                throw new JiraException(404, "User not found");

            var result = mapper.Map<UserResultDto>(user);
            result.Image = mapper.Map<UserImageResultDto>(
                await this.userImageRepository.SelectAsync(t => t.UserId.Equals(result.Id)));

            return result;
        }

        public async ValueTask<UserImageResultDto> GetUserImageAsync(long userId)
        {
            var userImage = await this.userImageRepository.SelectAsync(t => t.UserId.Equals(userId));
            if (userImage is null)
                throw new JiraException(404, "Image is not found");
            return mapper.Map<UserImageResultDto>(userImage);
        }

        public async ValueTask<UserImageResultDto> ImageUploadAsync(UserImageCreationDto dto)
        {
            var user = await this.userRepository.SelectAsync(t => t.Id.Equals(dto.UserId));
            if (user is null)
                throw new JiraException(404, "User is not found");

            byte[] image = dto.Image.ToByteArray();
            var fileExtension = Path.GetExtension(dto.Image.FileName);
            var fileName = Guid.NewGuid().ToString("N") + fileExtension;
            var webRootPath = EnvironmentHelper.WebHostPath;
            var folder = Path.Combine(webRootPath, "uploads", "images");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var fullPath = Path.Combine(folder, fileName);
            using var imageStream = new MemoryStream(image);

            using var imagePath = new FileStream(fullPath, FileMode.CreateNew);
            imageStream.WriteTo(imagePath);

            var userImage = new UserImage
            {
                Name = fileName,
                Path = fullPath,
                UserId = dto.UserId,
                User = user,
                CreatedAt = DateTime.UtcNow,
            };

            var createdImage = await this.userImageRepository.InsertAsync(userImage);
            await this.userImageRepository.SaveChangesAsync();
            return mapper.Map<UserImageResultDto>(createdImage);
        }

        public async ValueTask<UserResultDto> UpdateAsync(UserUpdateDto dto)
        {
            var updatingUser = await userRepository.SelectAsync(u => u.Id.Equals(dto.Id));
            if (updatingUser is null)
                throw new JiraException(404, "User not found");

            this.mapper.Map(dto, updatingUser);
            updatingUser.UpdatedAt = DateTime.UtcNow;
            await this.userRepository.SaveChangesAsync();

            var result = mapper.Map<UserResultDto>(updatingUser);
            result.Image = mapper.Map<UserImageResultDto>(
               await this.userImageRepository.SelectAsync(t => t.UserId.Equals(result.Id)));

            return result;
        }

        public ValueTask<UserResultDto> UserVerify(string code)
        {
            throw new NotImplementedException();
        }
    }
}
