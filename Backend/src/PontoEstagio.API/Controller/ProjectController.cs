using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontoEstagio.Application.UseCases.Project.GetCurrentProjectForUser;
using PontoEstagio.Application.UseCases.Projects.AssignUserToProject;
using PontoEstagio.Application.UseCases.Projects.DeleteUserFromProject;
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
public class ProjectController : ControllerBase
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

    [Authorize(Roles = nameof(UserType.Admin))]
    [HttpGet]
    [Route("GetCurrentProjectForUser")]
    [ProducesResponseType(typeof(ResponseProjectJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCurrentProjectForUser(
        [FromServices] IGetCurrentProjectForUserUseCase useCase
    )
    {
        var response = await useCase.Execute();
        return Ok(response);
    }

    [Authorize(Roles = nameof(UserType.Admin))]
    [HttpPost] 
    [ProducesResponseType(typeof(ResponseShortProjectJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterProjectUseCase useCase,
        [FromBody] RequestRegisterProjectJson request
    )
    {
        var response = await useCase.Execute(request);
        return Ok(response);
    }

    [Authorize(Roles = nameof(UserType.Admin))]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ResponseShortProjectJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateProjectUseCase useCase,
        [FromBody] RequestRegisterProjectJson request,
        [FromRoute] Guid id
    )
    {
        await useCase.Execute(id,request);
        return NoContent();
    }

    [Authorize(Roles = nameof(UserType.Admin))]
    [HttpPost("{projectId}/users")]
    [ProducesResponseType(typeof(ResponseShortProjectJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> AssignUserToProject(
        [FromServices] IAssignUserToProjectUseCase useCase,
        [FromBody] RequestAssignUserToProjectJson request,
        [FromRoute] Guid projectId
    )
    {
        await useCase.Execute(projectId, request);
        return NoContent();
    }

    [HttpPatch("{id}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateProjectStatus(
    [FromRoute] Guid id,
    [FromBody] RequestUpdateProjectStatusJson request,
    [FromServices] IUpdateProjectStatusUseCase useCase)
    {
      
        await useCase.Execute(id, request.Status);
        return NoContent();
    }

    [Authorize(Roles = nameof(UserType.Admin))]
    [HttpDelete("{projectId}/intern/{Intern_Id}/supervisor/{Supervisor_Id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RemoveUserFromProject(
    [FromServices] IDeleteUserFromProjectUseCase useCase,
    [FromRoute] Guid projectId,
    [FromRoute] Guid Intern_Id,
    [FromRoute] Guid Supervisor_Id
    )
    { 
        await useCase.Execute(projectId, Intern_Id, Supervisor_Id);
        return NoContent();
    }


}
