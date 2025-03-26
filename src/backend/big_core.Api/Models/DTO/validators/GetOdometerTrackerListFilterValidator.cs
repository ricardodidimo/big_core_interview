using big_core.Api.Helpers;
using big_core.Api.Models.DTO;
using big_core.Common;
using FluentValidation;
using System;

namespace big_core.Api.Validators
{
    public class GetOdometerTrackerListFilterValidator : AbstractValidator<GetOdemeterTrackerListFilterDTO>
    {
        public GetOdometerTrackerListFilterValidator()
        {
            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage(ErrorMessages.REQUIRED_START_DATE)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ErrorMessages.FUTURE_DATE_ERROR)
                .LessThan(x => x.EndDate).WithMessage(ErrorMessages.BIGGER_START_DATE_ERROR)
                .GreaterThan(DateTime.UtcNow.AddDays(-CommonConstants.RangeLimitForOdometerTrackDateSearch))
                .WithMessage(ErrorMessages.DATE_OUT_OF_RANGE);

            RuleFor(x => x.EndDate)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ErrorMessages.FUTURE_DATE_ERROR)
                .GreaterThan(DateTime.UtcNow.AddDays(-CommonConstants.RangeLimitForOdometerTrackDateSearch))
                .NotEmpty().WithMessage(ErrorMessages.REQUIRED_END_DATE)
                .GreaterThan(x => x.StartDate).WithMessage(ErrorMessages.LOWER_END_DATE_ERROR);

            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(0).WithMessage(ErrorMessages.INVALID_PAGE_ACTIVE);

            RuleFor(x => x.Rows)
                .GreaterThan(0).WithMessage(ErrorMessages.INVALID_ROWS_ERROR);

            RuleForEach(x => x.IdTms)
                .NotEmpty().WithMessage(ErrorMessages.EMPTY_STRING_FILTER_ERROR);

            RuleForEach(x => x.LicensePlates)
                .NotEmpty().WithMessage(ErrorMessages.EMPTY_STRING_FILTER_ERROR);

            RuleForEach(x => x.DivisionIds)
                .NotEmpty().WithMessage(ErrorMessages.EMPTY_STRING_FILTER_ERROR);
        }
    }
}
