using Domain.Models;

namespace UserAPI.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
