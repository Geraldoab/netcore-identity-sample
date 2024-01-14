using UserAPI.Models;

namespace UserAPI.Domain.Interfaces
{
    public interface IEmailMessengerService
    {
        public void SendMessage(Email email);
    }
}
