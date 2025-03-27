namespace big_core.Api.Models.DTO;

public record GetOdometerTrackerListFilterDTO(
    DateTime StartDate,
    DateTime EndDate,
    string[]? IdTms,
    string[]? LicensePlates,
    int[]? DivisionIds,
    int Rows,
    int Page
);

