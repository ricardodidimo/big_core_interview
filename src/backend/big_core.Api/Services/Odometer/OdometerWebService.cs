using big_core.Api.Models.DTO;
using big_core.Api.Models.DTO.Mappers;
using big_core.Api.Repository.Odometer;
using FluentResults;
using FluentValidation;

namespace big_core.Api.Services
{
    public class OdometerWebService(IOdometerRepository odometerRepository, IValidator<GetOdometerTrackerListFilterDTO> validator) : IOdometerService
    {
        private readonly IOdometerRepository _odometerRepository = odometerRepository;
        private readonly IValidator<GetOdometerTrackerListFilterDTO> _validator = validator;

        public async Task<IResult<GetOdometerTrackListDTO>> GetTracker(GetOdometerTrackerListFilterDTO filter)
        {
            var validationResult = _validator.Validate(filter);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result.Fail<GetOdometerTrackListDTO>(errors);
            }

            var result = await _odometerRepository.GetTracker(filter);
            if (result.IsFailed)
            {
                return Result.Fail<GetOdometerTrackListDTO>(result.Errors);
            }

            GetOdometerTrackListResultDTO resultDTO = result.Value;
            GetOdometerTrackListDTO mappedData = OdometerMapper.MapToSummary(resultDTO);
            return Result.Ok(mappedData);
        }
    }
}
