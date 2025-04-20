using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontoEstagio.Application.UseCases.Activity.GetActivitiesByAttendanceId;
using PontoEstagio.Application.UseCases.Activity.GetActivityById;
using PontoEstagio.Communication.Responses;

namespace PontoEstagio.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class ActivityController : ControllerBase
{
    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActivityById(
        [FromServices] IGetActivityByIdUseCase useCase,
        [FromRoute] Guid id
    )
    {
        var response = await useCase.Execute(id);
        return Ok(response);
    }

    [Authorize]
    [HttpGet("{attendanceId}/attendance/")]
    [ProducesResponseType(typeof(IEnumerable<ResponseActivityJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActivitiesByAttendanceId(
        [FromServices] IGetActivitiesByAttendanceIdUseCase useCase,
        [FromRoute] Guid attendanceId
    )
    {
        var response = await useCase.Execute(attendanceId);
        return Ok(response);
    }
}