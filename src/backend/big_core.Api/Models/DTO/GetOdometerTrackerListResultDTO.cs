namespace big_core.Api.Models.DTO;

public record GetOdometerTrackerListResultDTO(
    List<OdometerData> Data,
    int TotalItems,
    int NumberOfRowPage,
    int PageActive,
    int TotalPages
);

public record OdometerData(
    int? VehicleId,
    string? VehicleIdTms,
    int? DivisionId,
    string? DivisionName,
    int? OperationId,
    string? OperationName,
    string? Connector,
    string? LicensePlate,
    int? DriverId,
    string? DriverName,
    string? DriverIdTms,
    int? DriverDivisionId,
    string? DriverDivisionName,
    int? DriverOperationId,
    string? DriverOperationName,
    DateTime? DateProcess,
    int? Interval,
    int? Timezone,
    bool Moving,
    string? Id,
    bool Delayed,
    int? Odometer,
    double? OdometerKm,
    bool Ignition,
    string? IgnitionStatus,
    int? Bearing,
    Point? Point,
    int? Speed,
    int? Quality,
    DateTime? Date
);

public record Point(double? Latitude, double? Longitude);