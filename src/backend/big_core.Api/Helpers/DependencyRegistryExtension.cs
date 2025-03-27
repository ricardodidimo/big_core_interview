using big_core.Api.Repository.Odometer;
using big_core.Api.Services;
using big_core.Common;

namespace big_core.Api.Helpers
{
    public static class DependencyRegistryExtension
    {
        public static IServiceCollection RegisterHttpFactoryInstance(this IServiceCollection services)
        {
            var baseUrl = Environment.GetEnvironmentVariable(CommonConstants.ApiBaseUrlKey)
            ?? throw new Exception($"{CommonConstants.ApiBaseUrlKey} is missing");
            var authHeaderToken = Environment.GetEnvironmentVariable(CommonConstants.ApiAuthHeaderTokenKey)
            ?? throw new Exception($"{CommonConstants.ApiAuthHeaderTokenKey} is missing");

            services.AddHttpClient<IOdometerRepository, OdometerHttpRepository>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Basic", authHeaderToken
                );
            });

            return services;
        }

        public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
        {
            services.RegisterHttpFactoryInstance();
            services.AddScoped<IOdometerRepository, OdometerHttpRepository>();
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IOdometerService, OdometerWebService>();
            return services;
        }
    }
}
