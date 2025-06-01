using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontoEstagio.Application.UseCases.University.GetAllUniversities;
using PontoEstagio.Application.UseCases.University.GetUniversityById;
using PontoEstagio.Application.UseCases.University.Register;
using PontoEstagio.Application.UseCases.University.Update;
using PontoEstagio.Communication.Enum;
using PontoEstagio.Communication.Request;
using PontoEstagio.Communication.Responses;

namespace PontoEstagio.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class UniversityController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<ResponseUniversityJson>), StatusCodes.Status200OK)] 
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)] 
    public async Task<IActionResult> GetAllUniversities(
        [FromServices] IGetAllUniversitiesUseCase useCase)
    {
        var response = await useCase.Execute();
        return Ok(response);
    }

    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseUniversityJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)] 
    public async Task<IActionResult> GetUniversityById(
         [FromServices] IGetUniversityByIdUseCase useCase,
         [FromRoute] Guid id
    )
    {
        var response = await useCase.Execute(id);
        return Ok(response);
    }

    [Authorize(Roles = nameof(UserType.Admin))]
    [HttpPost] 
    [ProducesResponseType(typeof(ResponseUniversityJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUniversityUseCase useCase,
        [FromBody] RequestRegisterUniversityJson request
    )
    {
        var response = await useCase.Execute(request);
        return Ok(response);
    }

    [Authorize(Roles = nameof(UserType.Admin))]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateUniversityUseCase useCase,
        [FromBody] RequestRegisterUniversityJson request,
        [FromRoute] Guid id
    )
    {
        await useCase.Execute(id,request);
        return NoContent();
    }

}