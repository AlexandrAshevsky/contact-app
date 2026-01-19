using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Contact.Database
{
    public static class ServicesExtension
    {
        private const string DbConnectionKey = "ContactDbConnection";

        public static IServiceCollection AddContactDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var dbConnection = configuration.GetConnectionString(DbConnectionKey);

            services.AddDbContext<ContactDbContext>(options => 
                options.UseNpgsql(dbConnection)
            );

            return services;
        }
    }
}
