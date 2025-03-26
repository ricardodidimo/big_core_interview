using big_core.Api.Repository.Odometer;

namespace big_core.Api.Helpers
{
    public static class DependencyRegistryExtension
    {
        public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOdometerRepository, OdometerHttpRepository>();
            return services;
        }
    }
}
