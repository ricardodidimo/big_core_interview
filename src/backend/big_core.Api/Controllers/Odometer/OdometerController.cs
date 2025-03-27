namespace big_core.Api.Controllers.Odometer;

using big_core.Api.Models.DTO;
using big_core.Api.Services.Odometer;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Endpoint para obter informações do odômetro dos veículos.
/// </summary>
[ApiController]
[Route("api/odometer")]
public class OdometerController(IOdometerService odometerService) : ControllerBase
{
    private readonly IOdometerService _odometerService = odometerService;

    /// <summary>
    /// Obtém a lista de rastreamento do odômetro com base nos filtros fornecidos.
    /// </summary>
    /// <param name="filter">Objeto contendo os filtros da pesquisa.</param>
    /// <returns>Lista de rastreamento do odômetro conforme os filtros aplicados.</returns>
    /// <response code="200">Retorna a lista de rastreamento do odômetro.</response>
    /// <response code="400">Se os filtros forem inválidos.</response>
    /// <response code="500">Erro interno do servidor.</response>
    /// <remarks>
    /// Sample request:
    ///
    /// GET /api/odometer/tracker?StartDate=2025-03-20T20:01:19.869Z&amp;EndDate=2025-03-25T20:01:19.869Z&amp;Rows=10&amp;page=0   
    /// {
    ///     StartDate: 2025-03-20T20:01:19.869Z
    ///      EndDate: 2025-03-25T20:01:19.869Z
    ///      Rows: 10
    ///      page: 0 
    /// }
    /// </remarks>
    [HttpGet("tracker")]
    [ProducesResponseType(typeof(GetOdometerTrackerListDTO), 200)]
    [ProducesResponseType(typeof(List<FluentResults.IError>), 400)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<IActionResult> GetOdometerTracker([FromQuery] GetOdometerTrackerListFilterDTO filter)
    {
        var result = await _odometerService.GetTrackerAsync(filter);

        if (result.IsFailed)
            return BadRequest(result.Errors);

        return Ok(result.Value);
    }
}
