using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontoEstagio.Application.UseCases.Attendance.Register;
using PontoEstagio.Communication.Request;
using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Enum;

namespace PontoEstagio.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class AttendanceController : ControllerBase
{
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
}