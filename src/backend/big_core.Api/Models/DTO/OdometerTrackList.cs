namespace big_core.Api.Models.DTO;

public record GetOdometerTrackListDTO(
    List<OdometerSummaryDTO> Data,
    int TotalItems,
    int NumberOfRowPage,
    int PageActive,
    int TotalPages
);

public record OdometerSummaryDTO(
    int VehicleId,
    string VehicleIdTms,
    int OperationId,
    string OperationName,
    int DivisionId,
    string DivisionName,
    string LicensePlate,
    double? OdometerKm,
    int Speed,
    bool Moving,
    bool Ignition,
    int? DriverId,
    string? DriverName,
    DateTime DateProcess
);
