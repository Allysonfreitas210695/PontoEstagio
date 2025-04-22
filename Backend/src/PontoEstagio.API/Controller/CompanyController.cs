using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using PontoEstagio.Application.UseCases.Company.GetAllCompany;
using PontoEstagio.Application.UseCases.Company.Register; 
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
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status403Forbidden)]
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
    [ProducesResponseType(typeof(ResponseShortProjectJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAllCompany(
         [FromServices] IGetAllCompanyUseCase useCase
     )
    {
        var response = await useCase.Execute();
        return Ok(response);
    }
}
