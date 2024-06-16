using TND.Domain.Models;

namespace TND.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest emailRequest, CancellationToken cancellationToken = default);
    }
}
