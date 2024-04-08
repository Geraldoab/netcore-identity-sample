using Domain.Dtos;

namespace UserAPI.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<CustomResultDto> AddAsync(CreateUserDto dto);
        Task<CustomResultDto> LoginAsync(UserLoginDto dto);
    }
}
