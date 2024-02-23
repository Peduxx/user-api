using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Api.OptionsSetup
{
    public static class DatabaseOptionsSetup
    {
        private const string SectionName = "DefaultConnection";

        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<UserContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(SectionName)));

            services.AddDbContext<UserContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(SectionName)));
        }
    }
}
