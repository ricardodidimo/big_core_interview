namespace big_core.Tests.Unit.Services.OdometerService;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using big_core.Api.Models.DTO;
using big_core.Api.Repository.Odometer;

public class FakeOdometerRepository : IOdometerRepository
{
    private readonly List<OdometerData> _fakeData;

    public FakeOdometerRepository()
    {
        _fakeData = [
            new OdometerData(
                266483,
                "RTS1I59",
                42,
                "GLP",
                21,
                "GLP",
                "",
                "",
                null,
                null,
                null,
                null,
                "omnilink",
                null,
                null,
                DateTime.UtcNow,
                1742986800,
                -3,
                true,
                "266483-1742986800-omnilink",
                false,
                null,
                15,
                true,
                "Ligado",
                -1,
                new Point(-18.215694, -49.27125),
                10,
                1,
                DateTime.UtcNow),
        ];
    }

    public async Task<IResult<GetOdometerTrackListResultDTO>> GetTracker(GetOdometerTrackerListFilterDTO filter)
    {
        await Task.Delay(10);
        var filteredData = _fakeData
            .Where(x => filter.DivisionIds.Length is 0 || filter.DivisionIds.Contains(x.DivisionId))
            .Where(x => filter.LicensePlates.Length is 0 || filter.LicensePlates.Contains(x.LicensePlate))
            .Where(x => filter.IdTms.Length is 0 || filter.IdTms.Contains(x.VehicleIdTms))
            .Skip(filter.Page * filter.Rows)
            .Take(filter.Rows)
            .ToList();

        var result = new GetOdometerTrackListResultDTO
        (
            filteredData,
            _fakeData.Count,
            filter.Rows,
            filter.Page,
            (int)Math.Ceiling((double)_fakeData.Count / filter.Rows)
        );

        return Result.Ok(result);
    }
}
