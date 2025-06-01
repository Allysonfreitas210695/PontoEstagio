using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontoEstagio.Application.UseCases.Cource.GetAllCources;
using PontoEstagio.Application.UseCases.Cource.GetCourceById;
using PontoEstagio.Application.UseCases.Cource.Register;
using PontoEstagio.Application.UseCases.Cource.Update;
using PontoEstagio.Communication.Enum;
using PontoEstagio.Communication.Request;
using PontoEstagio.Communication.Responses;

namespace PontoEstagio.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class CourceController : ControllerBase
{

    [HttpGet]
    [ProducesResponseType(typeof(List<ResponseCourceJson>), StatusCodes.Status200OK)] 
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)] 
    public async Task<IActionResult> GetAllCources(
        [FromServices] IGetAllCourcesUseCase useCase)
    {
        var response = await useCase.Execute();
        return Ok(response);
    }

    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseCourceJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)] 
    public async Task<IActionResult> GetCourceById(
         [FromServices] IGetCourceByIdUseCase useCase,
         [FromRoute] Guid id
    )
    {
        var response = await useCase.Execute(id);
        return Ok(response);
    }

    [Authorize(Roles = nameof(UserType.Admin))]
    [HttpPost] 
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterCourceUseCase useCase,
        [FromBody] RequestRegisterCourceJson request
    )
    {
        await useCase.Execute(request);
        return NoContent();
    }

    [Authorize(Roles = nameof(UserType.Admin))]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateCourceUseCase useCase,
        [FromBody] RequestRegisterCourceJson request,
        [FromRoute] Guid id
    )
    {
        await useCase.Execute(id,request);
        return NoContent();
    }
}