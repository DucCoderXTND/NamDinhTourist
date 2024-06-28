using Microsoft.Extensions.DependencyInjection;
using TND.Domain.Interfaces.Services;

namespace TND.Infrastructure.Services.Pdf
{
    public static class PdfConfiguration
    {
        public static IServiceCollection AddPdfInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IPdfService, PdfService>();
            
            return services;
        }
    }
}
