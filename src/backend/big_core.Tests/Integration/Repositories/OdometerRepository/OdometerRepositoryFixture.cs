namespace big_core.Tests.Integration.Repositories.OdometerRepository;

using Microsoft.Extensions.DependencyInjection;
using big_core.Api.Repository.Odometer;
using big_core.Api.Helpers;

[CollectionDefinition("OdometerRepository Collection")]
public class OdometerRepositoryCollection : ICollectionFixture<OdometerRepositoryFixture> { }

public class OdometerRepositoryFixture
{
    public IOdometerRepository OdometerRepository { get; }

    public OdometerRepositoryFixture()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.RegisterHttpFactoryInstance();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        OdometerRepository = serviceProvider.GetRequiredService<IOdometerRepository>();
    }
}
