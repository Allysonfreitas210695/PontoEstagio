using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontoEstagio.Application.UseCases.Attendance.GetAllAttendances;
using PontoEstagio.Application.UseCases.Attendance.GetAttendanceById;
using PontoEstagio.Application.UseCases.Attendance.Register;
using PontoEstagio.Application.UseCases.Attendance.UpdateStatus;
using PontoEstagio.Communication.Request;
using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Enum;

namespace PontoEstagio.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class AttendanceController : ControllerBase
{
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(List<ResponseAttendanceJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAttendances(
        [FromServices] IGetAllAttendancesUseCase useCase)
    {
        var response = await useCase.Execute();
        return Ok(response);
    }

    [Authorize(Roles = nameof(UserType.Intern))]
    [HttpPost] 
    [ProducesResponseType(typeof(ResponseShortAttendanceJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterAttendanceUseCase useCase,
        [FromBody] RequestRegisterAttendanceJson request
    )
    {
        var response = await useCase.Execute(request);
        return Ok(response);
    }

    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseAttendanceJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        [FromServices] IGetAttendanceByIdUseCase useCase)
    {
        var response = await useCase.Execute(id);
        return Ok(response);
    }
    
    [Authorize(Roles = nameof(UserType.Supervisor))]
    [HttpPatch("{id}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProjectStatus(
    [FromRoute] Guid id,
    [FromBody] RequestUpdateAttendanceStatusJson request,
    [FromServices] IUpdateAttendanceStatusUseCase useCase)
    {
      
        await useCase.Execute(id, request);
        return NoContent();
    }
}