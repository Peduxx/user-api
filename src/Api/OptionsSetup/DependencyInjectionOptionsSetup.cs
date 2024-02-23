using Infrastructure.CrossCutting.IoC;

namespace Api.OptionsSetup
{
    public static class DependencyInjectionOptionsSetup
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            NativeInjector.RegisterServices(services);
        }
    }
}
