using Hotel.Core.Application.Models;

namespace Hotel.Core.Application.Interfaces.Infrastructure.Utils
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
