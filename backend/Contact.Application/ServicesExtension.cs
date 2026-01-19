using Contact.Application.Implementations;
using Contact.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Contact.Application
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IContactService, ContactService>();

            return services;
        }
    }
}
