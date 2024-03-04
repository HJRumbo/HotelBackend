using Hotel.Core.Application.Interfaces.Infrastructure.Utils;
using Hotel.Core.Application.Settings;
using Hotel.Infrastructure.Utils.EmailHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Infrastructure.Utils
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureUtilsLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SendGridSettings>(configuration.GetSection("SendGridSettings"));

            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
