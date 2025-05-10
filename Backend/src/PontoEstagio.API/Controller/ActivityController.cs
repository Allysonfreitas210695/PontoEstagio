using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontoEstagio.Application.UseCases.Activity.ActivitiesByProject;
using PontoEstagio.Application.UseCases.Activity.GetActivitiesByAttendanceId;
using PontoEstagio.Application.UseCases.Activity.GetActivitiesByUser;
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
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)] 
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
    [ProducesResponseType(typeof(List<ResponseActivityJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActivitiesByAttendanceId(
        [FromServices] IGetActivitiesByAttendanceIdUseCase useCase,
        [FromRoute] Guid attendanceId
    )
    {
        var response = await useCase.Execute(attendanceId);
        return Ok(response);
    }

    [Authorize]
    [HttpGet("{projectId}/project/")]
    [ProducesResponseType(typeof(List<ResponseActivityJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)] 
    public async Task<IActionResult> GetActivitiesByProject(
        [FromServices] IGetActivitiesByProjectUseCase useCase,
        [FromRoute] Guid projectId
    )
    {
        var response = await useCase.Execute(projectId);
        return Ok(response);
    }

    [Authorize]
    [HttpGet("{userId}/user/")]
    [ProducesResponseType(typeof(List<ResponseActivityJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetActivitiesByUser(
        [FromServices] IGetActivitiesByUserUseCase useCase,
        [FromRoute] Guid userId
    )
    {
        var response = await useCase.Execute(userId);
        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateActivity(
        [FromServices] ICreateActivityUseCase useCase,
        [FromForm] CreateActivityRequest request
    )
    {
        var response = await useCase.ExecuteAsync(request);
        return CreatedAtAction(nameof(GetActivityById), new { id = response.Id }, response);
    }
}