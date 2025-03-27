namespace big_core.Api.Services.Odometer;

using big_core.Api.Models.DTO;
using FluentResults;
using System.Threading.Tasks;

public interface IOdometerService
{
    Task<IResult<GetOdometerTrackerListDTO>> GetTrackerAsync(GetOdometerTrackerListFilterDTO filter);
}
