using big_core.Api.Models.DTO;
using FluentResults;

namespace big_core.Api.Repository.Odometer;

public interface IOdometerRepository
{
    public Task<IResult<GetOdometerTrackerListResultDTO>> GetTrackerAsync(GetOdometerTrackerListFilterDTO filterDTO);
}