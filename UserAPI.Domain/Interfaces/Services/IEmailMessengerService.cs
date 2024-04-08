using Domain.Models;

namespace UserAPI.Domain.Interfaces.Services
{
    public interface IEmailMessengerService
    {
        public void SendMessage(Email email);
    }
}
