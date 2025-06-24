using Microsoft.AspNetCore.Mvc;
using PontoEstagio.Application.UseCases.Auth.ForgotPassword;
using PontoEstagio.Application.UseCases.Auth.Refresh;
using PontoEstagio.Application.UseCases.Auth.ResetPassword;
using PontoEstagio.Application.UseCases.Login.DoLogin;
using PontoEstagio.Communication.Request;
using PontoEstagio.Communication.Responses;


namespace PontoEstagio.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(typeof(ResponseLoggedUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Register( 
        [FromServices] ILoginUserUseCase useCase,     
        [FromBody] RequestLoginUserJson request)
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }

    [HttpPost("refresh")]
    [ProducesResponseType(typeof(ResponseLoggedUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RefreshToken(
        [FromServices] IRefreshTokenUseCase useCase,
        [FromBody] RequestRefreshTokenJson request)
    {
        var response = await useCase.Execute(request);
        return Ok(response);
    }

    [HttpPost("forgot-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ForgotPassword(
        [FromServices] IForgotPasswordUseCase useCase,
        [FromBody] RequestForgotPasswordJson request)
    {
        await useCase.Execute(request);
        return NoContent();
    }

    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ResetPassword(
    [FromServices] IResetPasswordUseCase useCase,
    [FromBody] RequestResetPasswordJson request)
    {
        await useCase.Execute(request);
        return NoContent();
    }

}