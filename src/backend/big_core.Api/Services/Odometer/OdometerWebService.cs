namespace big_core.Api.Services.Odometer;

using big_core.Api.Helpers;
using big_core.Api.Models.DTO;
using big_core.Api.Models.DTO.Mappers;
using big_core.Api.Repository.Odometer;
using FluentResults;
using FluentValidation;

public class OdometerWebService(IOdometerRepository odometerRepository, IValidator<GetOdometerTrackerListFilterDTO> validator) : IOdometerService
{
    private readonly IOdometerRepository _odometerRepository = odometerRepository;
    private readonly IValidator<GetOdometerTrackerListFilterDTO> _validator = validator;

    public async Task<IResult<GetOdometerTrackerListDTO>> GetTrackerAsync(GetOdometerTrackerListFilterDTO filter)
    {
        var validationResult = _validator.Validate(filter);
        if (!validationResult.IsValid)
        {
            var errors = ValidationError.CreateErrorsList(validationResult.Errors);
            return Result.Fail<GetOdometerTrackerListDTO>(errors);
        }

        var result = await _odometerRepository.GetTrackerAsync(filter);
        if (result.IsFailed)
        {
            return Result.Fail<GetOdometerTrackerListDTO>(result.Errors);
        }

        GetOdometerTrackerListResultDTO resultDTO = result.Value;
        GetOdometerTrackerListDTO mappedData = OdometerMapper.MapToSummary(resultDTO);
        return Result.Ok(mappedData);
    }
}

