using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TND.Domain.Interfaces.Services;

namespace TND.Infrastructure.Services.Email
{
    public static class EmailConfiguration
    {
        public static IServiceCollection AddEmailInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailConfig>(configuration.GetSection(nameof(EmailConfig)));

            services.AddScoped<IEmailService, EmailService>();  

            return services;
        }
    }
}
