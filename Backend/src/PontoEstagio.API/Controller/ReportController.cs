using Microsoft.AspNetCore.Mvc;
using PontoEstagio.Application.UseCases.Reports.Monthly;
using PontoEstagio.Communication.Responses;

namespace PontoEstagio.API.Controller;
[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ResponseReportMonthlyJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<ResponseReportMonthlyJson>>> GerarRelatorio( 
        [FromQuery] string periodo,
        [FromServices] IReportsMonthlyUseCase _reportsMonthlyUseCase
    )
    {
        var relatorio = await _reportsMonthlyUseCase.Executar(periodo);
        return Ok(relatorio);
    }
}
