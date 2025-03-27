using big_core.Api.Models.DTO;
using big_core.Api.Repository.Odometer;
using big_core.Api.Services.Odometer;
using big_core.Api.Validators;
using big_core.Common;
using FluentValidation;

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
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<GetOdometerTrackerListFilterDTO>, GetOdometerTrackerListFilterValidator>();
            services.AddScoped<IOdometerService, OdometerWebService>();
            return services;
        }
    }
}
