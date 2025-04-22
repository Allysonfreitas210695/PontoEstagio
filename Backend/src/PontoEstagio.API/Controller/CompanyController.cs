using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using PontoEstagio.Application.UseCases.Company.GetAllCompany;
using PontoEstagio.Application.UseCases.Company.GetCompanyById;
using PontoEstagio.Application.UseCases.Company.Register;
using PontoEstagio.Application.UseCases.Company.Update;
using PontoEstagio.Communication.Request;
using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Enum;

namespace PontoEstagio.API.Controller;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
    [Authorize(Roles = nameof(UserType.Admin))]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseCompanyJson), StatusCodes.Status200OK)] 
    public async Task<IActionResult> Register(
         [FromServices] IRegisterCompanyUseCase useCase,
         [FromBody] RequestRegisterCompanytJson request
     )
    {
        var response = await useCase.Execute(request);
        return Ok(response);
    }

    [Authorize(Roles = nameof(UserType.Admin))]
    [HttpGet]
    [ProducesResponseType(typeof(List<ResponseShortProjectJson>), StatusCodes.Status200OK)] 
    public async Task<IActionResult> GetAllCompany(
         [FromServices] IGetAllCompanyUseCase useCase
     )
    {
        var response = await useCase.Execute();
        return Ok(response);
    }

    [Authorize(Roles = nameof(UserType.Admin))]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseShortProjectJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)] 
    public async Task<IActionResult> GetCompanyById(
         [FromServices] IGetCompanyByIdUseCase useCase,
         [FromRoute] Guid id
    )
    {
        var response = await useCase.Execute(id);
        return Ok(response);
    }

    [Authorize(Roles = nameof(UserType.Admin))]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)] 
    public async Task<IActionResult> Update(
         [FromServices] ICompanyUpdateUseCase useCase,
         [FromBody] RequestRegisterCompanytJson request,
         [FromRoute] Guid id
    )
    {
        await useCase.Execute(id, request);
        return NoContent();
    }
}
