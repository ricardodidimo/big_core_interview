namespace big_core.Tests.Integration.Controllers;

using System.Net;
using System.Net.Http.Json;
using big_core.Api.Models.DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

public class OdometerControllerTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task GetTrackerList_WithValidFilter_ShouldReturnOk()
    {
        var filter = new GetOdometerTrackerListFilterDTO(
            DateTime.UtcNow.AddDays(-3),
            DateTime.UtcNow,
            Array.Empty<string>(),
            Array.Empty<string>(),
            Array.Empty<int>(),
            10,
            0
        );

        HttpResponseMessage response = await _client.GetAsync($"/api/odometer/tracker?startDate={filter.StartDate:O}&endDate={filter.EndDate:O}&page={filter.Page}&rows={filter.Rows}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadFromJsonAsync<GetOdometerTrackListDTO>();
        content.Should().NotBeNull();
        content!.Data.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetTrackerList_WithInvalidStartDateFilter_ShouldReturnBadRequest()
    {
        var filter = new GetOdometerTrackerListFilterDTO(
            DateTime.UtcNow.AddDays(1),
            DateTime.UtcNow,
            Array.Empty<string>(),
            Array.Empty<string>(),
            Array.Empty<int>(),
            10,
            0
        );

        HttpResponseMessage response = await _client.GetAsync($"/api/odometer/tracker?startDate={filter.StartDate:O}&endDate={filter.EndDate:O}&page={filter.Page}&rows={filter.Rows}");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        string errorMessage = await response.Content.ReadAsStringAsync();
        errorMessage.Should().Contain("start date cannot be in the future");
    }
}

