namespace big_core.Api.Models.DTO.Mappers;

public static class OdometerMapper
{
    public static GetOdometerTrackerListDTO MapToSummary(GetOdometerTrackerListResultDTO resultDto)
    {
        return new GetOdometerTrackerListDTO(
            Data: resultDto.Data.Select(MapToSummary).ToList(),
            TotalItems: resultDto.TotalItems,
            NumberOfRowPage: resultDto.NumberOfRowPage,
            PageActive: resultDto.PageActive,
            TotalPages: resultDto.TotalPages
        );
    }

    private static OdometerSummaryDTO MapToSummary(OdometerData data)
    {
        return new OdometerSummaryDTO(
            VehicleId: data.VehicleId,
            VehicleIdTms: data.VehicleIdTms,
            OperationId: data.OperationId,
            OperationName: data.OperationName,
            DivisionId: data.DivisionId,
            DivisionName: data.DivisionName,
            LicensePlate: data.LicensePlate,
            OdometerKm: data.OdometerKm,
            Speed: data.Speed,
            Moving: data.Moving,
            Ignition: data.Ignition,
            DriverId: data.DriverId,
            DriverName: data.DriverName,
            DateProcess: data.DateProcess
        );
    }
}
