using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontoEstagio.Application.UseCases.Users.CheckUserExists;
using PontoEstagio.Application.UseCases.Users.Deactivated;
using PontoEstagio.Application.UseCases.Users.Delete;
using PontoEstagio.Application.UseCases.Users.GetAllUsers;
using PontoEstagio.Application.UseCases.Users.GetUserById;
using PontoEstagio.Application.UseCases.Users.Register;
using PontoEstagio.Application.UseCases.Users.Update;
using PontoEstagio.Communication.Request;
using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Enum;

namespace PontoEstagio.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpPost("check-user")]
    [ProducesResponseType(typeof(ResponseCheckUserUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CheckIfUserExists(
        [FromServices] ICheckUserExistsUseCase useCase,
        [FromBody] RequestCheckUserExistsUserJson request
    )
    { 
        return Ok(await useCase.Execute(request));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseLoggedUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register( 
        [FromServices] IRegisterUserUseCase useCase,     
        [FromBody] RequestRegisterUserJson request)
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(List<ResponseShortUserJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllUsers(
        [FromServices] IGetAllUsersUseCase useCase 
    )
    {
        var response = await useCase.Execute();
        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUserById(
        [FromServices] IGetUserByIdUseCase useCase,
        [FromRoute] Guid id
    )
    {
        var response = await useCase.Execute(id);
        return Ok(response);
    }

    [Authorize]
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateUserUseCase useCase,
        [FromRoute] Guid id,
        [FromBody] RequestRegisterUserJson request
    )
    {
        await useCase.Execute(id, request);
        return NoContent();
    }

    [Authorize]
    [HttpPatch]
    [Route("/{id}/activate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Activate(
        [FromServices] IActivateUserUseCase useCase,
        [FromRoute] Guid id
    )
    {
        await useCase.Execute(id);
        return NoContent();
    }

    [Authorize]
    [HttpPatch]
    [Route("/{id}/deactiveted")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Deactiveted(
        [FromServices] IDeactivatedUserUseCase useCase,
        [FromRoute] Guid id
    )
    {
        await useCase.Execute(id);
        return NoContent();
    }
}