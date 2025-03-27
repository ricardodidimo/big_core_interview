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
    [HttpGet("tracker")]
    [ProducesResponseType(typeof(GetOdometerTrackListDTO), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<IActionResult> GetOdometerTracker([FromQuery] GetOdometerTrackerListFilterDTO filter)
    {
        var result = await _odometerService.GetTrackerAsync(filter);

        if (result.IsFailed)
            return BadRequest(result.Errors);

        return Ok(result.Value);
    }
}
