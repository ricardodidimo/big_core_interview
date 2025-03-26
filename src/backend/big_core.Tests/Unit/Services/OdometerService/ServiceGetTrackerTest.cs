namespace big_core.Tests.Unit.Services.OdometerService;

using big_core.Api.Helpers;
using big_core.Api.Models.DTO;
using big_core.Common;
using FluentAssertions;
using FluentResults;


public class ServiceGetTrackerTest
{
    private readonly IOdometerService _odometerService;

    private readonly FakeOdometerRepository _odometerRepository = new();

    [Fact]
    public async Task ServiceGetTracker_OnSuccess_ShouldReturnOkAsExpected()
    {
        GetOdemeterTrackerListFilterDTO getTrackerInput = new(
            DateTime.UtcNow.Subtract(TimeSpan.FromDays(3)),
            DateTime.UtcNow,
            Array.Empty<string>(),
            Array.Empty<string>(),
            Array.Empty<int>(),
            10,
            0
        );

        IResult<OdometerTrackList> result = await _odometerService.GetTracker(getTrackerInput);
        result.IsFailed.Should().BeFalse();
    }

    [Fact]
    public async Task ServiceGetTracker_OnStartDateBiggerThanCurrentDate_ShouldReturnValidationError()
    {
        GetOdemeterTrackerListFilterDTO getTrackerInput = new(
            DateTime.UtcNow.AddDays(1),
            DateTime.UtcNow,
            Array.Empty<string>(),
            Array.Empty<string>(),
            Array.Empty<int>(),
            10,
            0
        );

        IResult<OdometerTrackList> result = await _odometerService.GetTracker(getTrackerInput);
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().Contain(e => e.Message.Contains(ErrorMessages.FUTURE_DATE_ERROR));
    }

    [Fact]
    public async Task ServiceGetTracker_OnStartDateBiggerThanEndDate_ShouldReturnValidationError()
    {
        GetOdemeterTrackerListFilterDTO getTrackerInput = new(
            DateTime.UtcNow,
            DateTime.UtcNow.Subtract(TimeSpan.FromDays(1)),
            Array.Empty<string>(),
            Array.Empty<string>(),
            Array.Empty<int>(),
            10,
            0
        );

        IResult<OdometerTrackList> result = await _odometerService.GetTracker(getTrackerInput);
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().Contain(e => e.Message.Contains(ErrorMessages.BIGGER_START_DATE_ERROR));
    }

    [Fact]
    public async Task ServiceGetTracker_OnStartDateLowerThanLimit_ShouldReturnValidationError()
    {
        DateTime limitDate = DateTime.UtcNow.Subtract(TimeSpan.FromDays(CommonConstants.RangeLimitForOdometerTrackDateSearch));

        GetOdemeterTrackerListFilterDTO getTrackerInput = new(
            limitDate.Subtract(TimeSpan.FromDays(1)),
            DateTime.UtcNow,
            Array.Empty<string>(),
            Array.Empty<string>(),
            Array.Empty<int>(),
            10,
            0
        );

        IResult<OdometerTrackList> result = await _odometerService.GetTracker(getTrackerInput);
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().Contain(e => e.Message.Contains(ErrorMessages.DATE_OUT_OF_RANGE));
    }

    [Fact]
    public async Task ServiceGetTracker_OnFilterContainsEmptyStrings_ShouldReturnValidationError()
    {
        GetOdemeterTrackerListFilterDTO getTrackerInput = new(
            DateTime.UtcNow.Subtract(TimeSpan.FromDays(3)),
            DateTime.UtcNow,
            new string[] { "" },
            new string[] { "" },
            Array.Empty<int>(),
            10,
            0
        );

        IResult<OdometerTrackList> result = await _odometerService.GetTracker(getTrackerInput);
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().Contain(e => e.Message.Contains(ErrorMessages.EMPTY_STRING_FILTER_ERROR));
    }

    [Fact]
    public async Task ServiceGetTracker_OnPageActiveNegative_ShouldReturnValidationError()
    {
        GetOdemeterTrackerListFilterDTO getTrackerInput = new(
            DateTime.UtcNow.Subtract(TimeSpan.FromDays(3)),
            DateTime.UtcNow,
            Array.Empty<string>(),
            Array.Empty<string>(),
            Array.Empty<int>(),
            10,
            -1
        );

        IResult<OdometerTrackList> result = await _odometerService.GetTracker(getTrackerInput);
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().Contain(e => e.Message.Contains(ErrorMessages.INVALID_PAGE_ACTIVE));
    }

    [Fact]
    public async Task ServiceGetTracker_OnRowsZeroOrNegative_ShouldReturnValidationError()
    {
        GetOdemeterTrackerListFilterDTO getTrackerInput = new(
            DateTime.UtcNow.Subtract(TimeSpan.FromDays(3)),
            DateTime.UtcNow,
            Array.Empty<string>(),
            Array.Empty<string>(),
            Array.Empty<int>(),
            -1,
            1
        );

        IResult<OdometerTrackList> result = await _odometerService.GetTracker(getTrackerInput);
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().Contain(e => e.Message.Contains(ErrorMessages.INVALID_ROWS_ERROR));

        getTrackerInput = new(
            DateTime.UtcNow.Subtract(TimeSpan.FromDays(3)),
            DateTime.UtcNow,
            Array.Empty<string>(),
            Array.Empty<string>(),
            Array.Empty<int>(),
            -1,
            1
        );

        result = await _odometerService.GetTracker(getTrackerInput);
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().Contain(e => e.Message.Contains(ErrorMessages.INVALID_ROWS_ERROR));
    }
}
