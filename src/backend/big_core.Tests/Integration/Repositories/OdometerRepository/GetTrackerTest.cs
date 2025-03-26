namespace big_core.Tests.Integration.Repositories.OdometerRepository;

using FluentAssertions;
using FluentResults;
using big_core.Api.Models.DTO;
using big_core.Api.Repository.Odometer;

[Collection("OdometerRepository Collection")]
public class GetTrackerTest(OdometerRepositoryFixture fixture)
{
    private readonly IOdometerRepository _odometerRepository = fixture.OdometerRepository;
    private readonly string CITROSUCO_ID = "39";
    private readonly string GLP_ID = "42";

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