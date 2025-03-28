namespace big_core.Api.Models.DTO;

public record GetOdometerTrackerListDTO(
    List<OdometerSummaryDTO> Data,
    int TotalItems,
    int NumberOfRowPage,
    int PageActive,
    int TotalPages
);

public enum VehicleStatus
{
    Stopped,
    Moving,
    Delayed_Moving,
    Delayed_Stopped
}

public record OdometerSummaryDTO(
    int? VehicleId,
    string? VehicleIdTms,
    int? OperationId,
    string? OperationName,
    int? DivisionId,
    string? DivisionName,
    string? LicensePlate,
    double? OdometerKm,
    int? Speed,
    VehicleStatus VehicleStatus,
    bool? Ignition,
    int? DriverId,
    string? DriverName,
    DateTime? DateProcess
);
