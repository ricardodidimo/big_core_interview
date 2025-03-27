namespace big_core.Tests.Unit.Services.OdometerService;

using big_core.Api.Helpers;
using big_core.Api.Models.DTO;
using big_core.Api.Services.Odometer;
using big_core.Api.Validators;
using big_core.Common;
using FluentAssertions;
using FluentResults;


public class ServiceGetTrackerTest
{
    private readonly FakeOdometerRepository _odometerRepository = new();
    private readonly IOdometerService _odometerService;

    public ServiceGetTrackerTest()
    {
        _odometerService = new OdometerWebService(_odometerRepository, new GetOdometerTrackerListFilterValidator());
    }

    [Fact]
    public async Task ServiceGetTracker_OnSuccess_ShouldReturnOkAsExpected()
    {
        GetOdometerTrackerListFilterDTO getTrackerInput = new(
            DateTime.UtcNow.Subtract(TimeSpan.FromDays(3)),
            DateTime.UtcNow.Subtract(TimeSpan.FromHours(2)),
            Array.Empty<string>(),
            Array.Empty<string>(),
            Array.Empty<int>(),
            10,
            0
        );

        IResult<GetOdometerTrackerListDTO> result = await _odometerService.GetTrackerAsync(getTrackerInput);
        result.IsFailed.Should().BeFalse();
    }

    [Fact]
    public async Task ServiceGetTracker_OnStartDateBiggerThanCurrentDate_ShouldReturnValidationError()
    {
        GetOdometerTrackerListFilterDTO getTrackerInput = new(
            DateTime.UtcNow.AddDays(1),
            DateTime.UtcNow,
            Array.Empty<string>(),
            Array.Empty<string>(),
            Array.Empty<int>(),
            10,
            0
        );

        IResult<GetOdometerTrackerListDTO> result = await _odometerService.GetTrackerAsync(getTrackerInput);
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().Contain(e =>
            ((IEnumerable<string>)e.Metadata[ValidationError.MESSAGES_METADATA_KEY])
            .Contains(ErrorMessages.FUTURE_DATE_ERROR)
        );
    }

    [Fact]
    public async Task ServiceGetTracker_OnStartDateBiggerThanEndDate_ShouldReturnValidationError()
    {
        GetOdometerTrackerListFilterDTO getTrackerInput = new(
            DateTime.UtcNow,
            DateTime.UtcNow.Subtract(TimeSpan.FromDays(1)),
            Array.Empty<string>(),
            Array.Empty<string>(),
            Array.Empty<int>(),
            10,
            0
        );

        IResult<GetOdometerTrackerListDTO> result = await _odometerService.GetTrackerAsync(getTrackerInput);
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().Contain(e =>
            ((IEnumerable<string>)e.Metadata[ValidationError.MESSAGES_METADATA_KEY])
            .Contains(ErrorMessages.BIGGER_START_DATE_ERROR)
        );
    }

    [Fact]
    public async Task ServiceGetTracker_OnStartDateLowerThanLimit_ShouldReturnValidationError()
    {
        DateTime limitDate = DateTime.UtcNow.Subtract(TimeSpan.FromDays(CommonConstants.RangeLimitForOdometerTrackDateSearch));

        GetOdometerTrackerListFilterDTO getTrackerInput = new(
            limitDate.Subtract(TimeSpan.FromDays(1)),
            DateTime.UtcNow,
            Array.Empty<string>(),
            Array.Empty<string>(),
            Array.Empty<int>(),
            10,
            0
        );

        IResult<GetOdometerTrackerListDTO> result = await _odometerService.GetTrackerAsync(getTrackerInput);
        result.IsFailed.Should().BeTrue();
        var expectedMessage = ErrorMessages.DATE_OUT_OF_RANGE.Replace("{PropertyName}", "Start Date");
        result.Errors.Should().Contain(e =>
            ((IEnumerable<string>)e.Metadata[ValidationError.MESSAGES_METADATA_KEY])
            .Contains(expectedMessage)
        );
    }

    [Fact]
    public async Task ServiceGetTracker_OnEndDateLowerThanStartDate_ShouldReturnValidationError()
    {
        GetOdometerTrackerListFilterDTO getTrackerInput = new(
            DateTime.UtcNow.Add(TimeSpan.FromDays(2)),
            DateTime.UtcNow,
            Array.Empty<string>(),
            Array.Empty<string>(),
            Array.Empty<int>(),
            10,
            0
        );

        IResult<GetOdometerTrackerListDTO> result = await _odometerService.GetTrackerAsync(getTrackerInput);
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().Contain(e =>
            ((IEnumerable<string>)e.Metadata[ValidationError.MESSAGES_METADATA_KEY])
            .Contains(ErrorMessages.LOWER_END_DATE_ERROR)
        );
    }

    [Fact]
    public async Task ServiceGetTracker_OnFilterContainsEmptyStrings_ShouldReturnValidationError()
    {
        GetOdometerTrackerListFilterDTO getTrackerInput = new(
            DateTime.UtcNow.Subtract(TimeSpan.FromDays(3)),
            DateTime.UtcNow.Subtract(TimeSpan.FromHours(2)),
            new string[] { "" },
            new string[] { "" },
            Array.Empty<int>(),
            10,
            0
        );

        IResult<GetOdometerTrackerListDTO> result = await _odometerService.GetTrackerAsync(getTrackerInput);
        result.IsFailed.Should().BeTrue();
        
        var expectedMessage = ErrorMessages.EMPTY_STRING_FILTER_ERROR.Replace("{PropertyName}", "Id Tms");
        result.Errors.Should().Contain(e =>
            ((IEnumerable<string>)e.Metadata[ValidationError.MESSAGES_METADATA_KEY])
            .Contains(expectedMessage)
        );
    }

    [Fact]
    public async Task ServiceGetTracker_OnPageActiveNegative_ShouldReturnValidationError()
    {
        GetOdometerTrackerListFilterDTO getTrackerInput = new(
            DateTime.UtcNow.Subtract(TimeSpan.FromDays(3)),
            DateTime.UtcNow,
            Array.Empty<string>(),
            Array.Empty<string>(),
            Array.Empty<int>(),
            10,
            -1
        );

        IResult<GetOdometerTrackerListDTO> result = await _odometerService.GetTrackerAsync(getTrackerInput);
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().Contain(e =>
            ((IEnumerable<string>)e.Metadata[ValidationError.MESSAGES_METADATA_KEY])
            .Contains(ErrorMessages.INVALID_PAGE_ACTIVE)
        );
    }

    [Fact]
    public async Task ServiceGetTracker_OnRowsZeroOrNegative_ShouldReturnValidationError()
    {
        GetOdometerTrackerListFilterDTO getTrackerInput = new(
            DateTime.UtcNow.Subtract(TimeSpan.FromDays(3)),
            DateTime.UtcNow.Subtract(TimeSpan.FromHours(2)),
            Array.Empty<string>(),
            Array.Empty<string>(),
            Array.Empty<int>(),
            -1,
            1
        );

        IResult<GetOdometerTrackerListDTO> result = await _odometerService.GetTrackerAsync(getTrackerInput);
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().Contain(e =>
            ((IEnumerable<string>)e.Metadata[ValidationError.MESSAGES_METADATA_KEY])
            .Contains(ErrorMessages.INVALID_ROWS_ERROR)
        );
    }
}
