namespace big_core.Tests.Integration.Repositories.OdometerRepository;

using Microsoft.Extensions.DependencyInjection;
using big_core.Api.Repository.Odometer;
using big_core.Common;

[CollectionDefinition("OdometerRepository Collection")]
public class OdometerRepositoryCollection : ICollectionFixture<OdometerRepositoryFixture> { }

public class OdometerRepositoryFixture
{
    public IOdometerRepository OdometerRepository { get; }

    public OdometerRepositoryFixture()
    {
        var baseUrl = Environment.GetEnvironmentVariable(CommonConstants.ApiBaseUrlKey) 
        ?? throw new Exception($"{CommonConstants.ApiBaseUrlKey} is missing");
        var authHeaderToken = Environment.GetEnvironmentVariable(CommonConstants.ApiAuthHeaderTokenKey) 
        ?? throw new Exception($"{CommonConstants.ApiAuthHeaderTokenKey} is missing");

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddHttpClient<IOdometerRepository, OdometerHttpRepository>(client =>
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Basic", authHeaderToken
            );
        });

        var serviceProvider = serviceCollection.BuildServiceProvider();
        OdometerRepository = serviceProvider.GetRequiredService<IOdometerRepository>();
    }
}
