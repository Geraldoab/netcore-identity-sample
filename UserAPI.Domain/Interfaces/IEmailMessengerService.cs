using Domain.Models;

namespace Domain.Interfaces
{
    public interface IEmailMessengerService
    {
        public void SendMessage(Email email);
    }
}
