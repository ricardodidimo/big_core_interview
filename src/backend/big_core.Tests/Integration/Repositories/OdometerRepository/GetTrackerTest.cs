namespace big_core.Tests.Integration.Repositories.OdometerRepository;

using FluentAssertions;
using FluentResults;
using big_core.Api.Models.DTO;
using big_core.Api.Repository.Odometer;

[Collection("OdometerRepository Collection")]
public class GetTrackerTest(OdometerRepositoryFixture fixture)
{
    private readonly IOdometerRepository _odometerRepository = fixture.OdometerRepository;
    private readonly int CITROSUCO_ID = 39;
    private readonly int GLP_ID = 42;
    private async Task<IResult<GetOdometerTrackListResultDTO>> ExecuteWithRetry(Func<Task<IResult<GetOdometerTrackListResultDTO>>> action)
    {
        int maxAttempts = 5;
        int delay = 2000;

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            var result = await action();
            if (result.IsSuccess) return result;

            await Task.Delay(delay);
            delay *= 2;
        }

        return Result.Fail<GetOdometerTrackListResultDTO>("Max retry attempts exceeded.");
    }

    [Fact]
    public async Task GetTrackerList_OnSuccess_ShouldReturnOkAsExpected()
    {
        GetOdometerTrackerListFilterDTO getTrackerInput = new(
            DateTime.UtcNow.Subtract(TimeSpan.FromDays(3)),
            DateTime.UtcNow,
            Array.Empty<string>(),
            Array.Empty<string>(),
            [CITROSUCO_ID, GLP_ID],
            10,
            1
        );

        IResult<GetOdometerTrackListResultDTO> result = await ExecuteWithRetry(() => _odometerRepository.GetTrackerAsync(getTrackerInput));
        result.IsFailed.Should().BeFalse();
    }

    [Fact]
    public async Task GetTrackerList_OnCustomNumberOfRowsPerPageSetted_ShouldReturnsExpectedCount()
    {
        GetOdometerTrackerListFilterDTO getTrackerInput = new(
            DateTime.UtcNow.Subtract(TimeSpan.FromDays(3)),
            DateTime.UtcNow,
            Array.Empty<string>(),
            Array.Empty<string>(),
            [CITROSUCO_ID, GLP_ID],
            2,
            1
        );

        IResult<GetOdometerTrackListResultDTO> result = await ExecuteWithRetry(() => _odometerRepository.GetTrackerAsync(getTrackerInput));
        result.IsFailed.Should().BeFalse();
        result.Value.Should().NotBeNull();
        result.Value.Data.Count.Should().Be(getTrackerInput.Rows);
    }

    [Fact]
    public async Task GetTrackerList_OnCustomActivePageSetted_ShouldReturnsExpectedIndex()
    {
        GetOdometerTrackerListFilterDTO getTrackerInput = new(
            DateTime.UtcNow.Subtract(TimeSpan.FromDays(3)),
            DateTime.UtcNow,
            Array.Empty<string>(),
            Array.Empty<string>(),
            [CITROSUCO_ID, GLP_ID],
            10,
            2
        );

        IResult<GetOdometerTrackListResultDTO> result = await ExecuteWithRetry(() => _odometerRepository.GetTrackerAsync(getTrackerInput));
        result.IsFailed.Should().BeFalse();
        result.Value.Should().NotBeNull();
        result.Value.PageActive.Should().Be(2);
    }
}