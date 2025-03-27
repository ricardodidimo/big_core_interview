namespace big_core.Api.Repository.Odometer;

using System.Text;
using System.Text.Json;
using big_core.Api.Helpers;
using big_core.Api.Models.DTO;
using FluentResults;

public class OdometerHttpRepository(HttpClient httpClient) : IOdometerRepository
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly string ODOMETER_RESOURCE_PATH = "Vehicles/TrackerOdometer";

    private string AppendQueryValues<T>(IEnumerable<T> values, string queryPrefix)
    {
        var queryBuilder = new StringBuilder();
        foreach (T value in values)
        {
            queryBuilder.Append($"&{queryPrefix}={value}");
        }

        return queryBuilder.ToString();
    }

    private string FormatGetTrackerUrl(GetOdometerTrackerListFilterDTO filter)
    {
        var appendBuilder = new StringBuilder($"{ODOMETER_RESOURCE_PATH}?StartDate={filter.StartDate:O}&EndDate={filter.EndDate:O}&Page={filter.Page}&Rows={filter.Rows}");
        appendBuilder.Append(AppendQueryValues(filter.DivisionIds, "DivisionId"));
        appendBuilder.Append(AppendQueryValues(filter.LicensePlates, "LicensePlate"));
        appendBuilder.Append(AppendQueryValues(filter.IdTms, "IdTms"));

        return appendBuilder.ToString();
    }

    public async Task<IResult<GetOdometerTrackListResultDTO>> GetTracker(GetOdometerTrackerListFilterDTO filter)
    {
        var url = FormatGetTrackerUrl(filter);
        HttpResponseMessage response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            return Result.Fail<GetOdometerTrackListResultDTO>(ErrorMessages.API_REQUEST_FAILED_ERROR);
        }

        var content = await response.Content.ReadAsStringAsync();
        GetOdometerTrackListResultDTO? data;
        try
        {
            data = JsonSerializer.Deserialize<GetOdometerTrackListResultDTO>(content, JsonSettings.DefaultOptions);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Deserialization error: {ex.Message}");
            return Result.Fail<GetOdometerTrackListResultDTO>(ErrorMessages.UNEXPECTED_API_RESPONSE_ERROR);
        }

        if (data is null)
        {
            return Result.Fail<GetOdometerTrackListResultDTO>(ErrorMessages.UNEXPECTED_API_RESPONSE_ERROR);
        }

        return Result.Ok(data);
    }
}