using Microsoft.AspNetCore.Mvc;
using PontoEstagio.Application.UseCases.Auth.Refresh;
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
    public async Task<IActionResult> Register( 
        [FromServices] ILoginUserUseCase useCase,     
        [FromBody] RequestLoginUserJson request)
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }

    [HttpPost("refresh")]
    [ProducesResponseType(typeof(ResponseLoggedUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RefreshToken(
        [FromServices] IRefreshTokenUseCase useCase,
        [FromBody] RequestRefreshTokenJson request)
    {
        var response = await useCase.Execute(request);
        return Ok(response);
    }
}