namespace big_core.Tests.Integration.Repositories.OdometerRepository;

using FluentAssertions;
using FluentResults;
using Microsoft.Extensions.DependencyInjection;
using big_core.Common;
using big_core.Api.Models.DTO;

public class GetTrackerTest
{
    private readonly IOdometerRepository _odometerRepository;
    private readonly string CITROSUCO_ID = "39";
    private readonly string GLP_ID = "42";

    public GetTrackerTest()
    {
        var baseUrl = Environment.GetEnvironmentVariable(CommonConstants.ApiBaseUrlKey) ?? throw new Exception("API_BASE_URL is missing");
        var authHeaderToken = Environment.GetEnvironmentVariable(CommonConstants.ApiAuthHeaderTokenKey) ?? throw new Exception("API_AUTH_HEADER_TOKEN is missing");

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddHttpClient<IOdometerRepository, OdometerHttpRepository>(client =>
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Basic", authHeaderToken
            );
        });

        var serviceProvider = serviceCollection.BuildServiceProvider();
        _odometerRepository = serviceProvider.GetRequiredService<IOdometerRepository>();
    }

    [Fact]
    public async Task GetTrackerList_OnSuccess_ShouldReturnOkAsExpected()
    {
        GetOdemeterTrackerListFilterDTO getTrackerInput = new(
            DateTime.UtcNow.Subtract(TimeSpan.FromDays(3)),
            DateTime.UtcNow,
            Array.Empty<string>(),
            Array.Empty<string>(),
            [CITROSUCO_ID, GLP_ID],
            10,
            1
        );

        IResult<GetOdometerTrackListResultDTO> result = await _odometerRepository.GetTracker(getTrackerInput);
        result.IsFailed.Should().BeFalse();
    }

    [Fact]
    public async Task GetTrackerList_OnCustomNumberOfRowsPerPageSetted_ShouldReturnsExpectedCount()
    {
        GetOdemeterTrackerListFilterDTO getTrackerInput = new(
            DateTime.UtcNow.Subtract(TimeSpan.FromDays(3)),
            DateTime.UtcNow,
            Array.Empty<string>(),
            Array.Empty<string>(),
            [CITROSUCO_ID, GLP_ID],
            2,
            1
        );

        IResult<GetOdometerTrackListResultDTO> result = await _odometerRepository.GetTracker(getTrackerInput);
        result.IsFailed.Should().BeFalse();
        result.Value.Should().NotBeNull();
        result.Value.Data.Count.Should().Be(getTrackerInput.Rows);
    }

    [Fact]
    public async Task GetTrackerList_OnCustomActivePageSetted_ShouldReturnsExpectedIndex()
    {
        GetOdemeterTrackerListFilterDTO getTrackerInput = new(
            DateTime.UtcNow.Subtract(TimeSpan.FromDays(3)),
            DateTime.UtcNow,
            Array.Empty<string>(),
            Array.Empty<string>(),
            [CITROSUCO_ID, GLP_ID],
            10,
            2
        );

        IResult<GetOdometerTrackListResultDTO> result = await _odometerRepository.GetTracker(getTrackerInput);
        result.IsFailed.Should().BeFalse();
        result.Value.Should().NotBeNull();
        result.Value.PageActive.Should().Be(2);
    }
}