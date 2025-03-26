namespace big_core.Api.Services;

using big_core.Api.Models.DTO;
using FluentResults;
using System.Threading.Tasks;

public interface IOdometerService
{
    Task<IResult<GetOdometerTrackListDTO>> GetTracker(GetOdemeterTrackerListFilterDTO filter);
}
