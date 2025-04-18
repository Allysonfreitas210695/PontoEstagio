using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontoEstagio.Application.UseCases.Projects.GetAllProjects;
using PontoEstagio.Application.UseCases.Projects.GetProjectById;
using PontoEstagio.Application.UseCases.Projects.Register;
using PontoEstagio.Application.UseCases.Projects.Update;
using PontoEstagio.Application.UseCases.Projects.UpdateStatus;
using PontoEstagio.Communication.Request;
using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Enum;

namespace PontoEstagio.API.Controller;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    [HttpGet] 
    [ProducesResponseType(typeof(List<ResponseShortProjectJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllProjectsByUser(
        [FromServices] IGetAllProjectsUseCase useCase
    )
    { 
        return Ok(await useCase.Execute());
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseProjectJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUserById(
        [FromServices] IGetProjectByIdUseCase useCase,
        [FromRoute] Guid id
    )
    {
        var response = await useCase.Execute(id);
        return Ok(response);
    }

    [Authorize(Roles = nameof(UserType.Supervisor))]
    [HttpPost] 
    [ProducesResponseType(typeof(ResponseShortProjectJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterProjectUseCase useCase,
        [FromBody] RequestRegisterProjectJson request
    )
    {
        var response = await useCase.Execute(request);
        return Ok(response);
    }

    [Authorize(Roles = nameof(UserType.Supervisor))]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ResponseShortProjectJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateProjectUseCase useCase,
        [FromBody] RequestRegisterProjectJson request,
        [FromRoute] Guid id
    )
    {
        await useCase.Execute(id,request);
        return NoContent();
    }

    [HttpPatch("{id}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProjectStatus(
    [FromRoute] Guid id,
    [FromBody] RequestUpdateProjectStatusJson request,
    [FromServices] IUpdateProjectStatusUseCase useCase)
    {
      
        await useCase.Execute(id, request.Status);
        return NoContent();
    }
}
