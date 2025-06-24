using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using PontoEstagio.Application.UseCases.Company.DeleteLegalRepresentative;
using PontoEstagio.Application.UseCases.Company.GetAllCompany;
using PontoEstagio.Application.UseCases.Company.GetAllRepresentativesFromCompany;
using PontoEstagio.Application.UseCases.Company.GetCompanyById;
using PontoEstagio.Application.UseCases.Company.GetLegalRepresentativeById;
using PontoEstagio.Application.UseCases.Company.Register;
using PontoEstagio.Application.UseCases.Company.RegisterLegalRepresentative;
using PontoEstagio.Application.UseCases.Company.Update;
using PontoEstagio.Application.UseCases.Company.UpdateLegalRepresentative;
using PontoEstagio.Communication.Request;
using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Enum;

namespace PontoEstagio.API.Controller;

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
    
    [HttpPost("{companyId}/representatives")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterRepresentative(
        [FromServices] IRegisterLegalRepresentativeUseCase useCase,
        [FromRoute] Guid companyId,
        [FromBody] RequestRegisterLegalRepresentativeJson request)
    {
        await useCase.Execute(request);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpGet("{companyId}/representatives/{representativeId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterRepresentative(
        [FromServices] IGetLegalRepresentativeByIdUseCase useCase,
        [FromRoute] Guid companyId,
        [FromRoute] Guid representativeId

        )
    {
        await useCase.Execute(companyId, representativeId);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpGet("{companyId}/representatives")]
    [ProducesResponseType(typeof(List<ResponseLegalRepresentativeJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllRepresentatives(
        [FromServices] IGetAllRepresentativesFromCompanyUseCase useCase,
        [FromRoute] Guid companyId)
    {
        var response = await useCase.Execute(companyId);
        return Ok(response);
    }

    [HttpPut("/representatives/{representativeId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateRepresentative(
        [FromServices] IUpdateLegalRepresentativeUseCase useCase,
        [FromRoute] Guid representativeId,
        [FromBody] RequestRegisterLegalRepresentativeJson request)
    {
        await useCase.Execute(representativeId, request);
        return NoContent();
    }

    [Authorize(Roles = nameof(UserType.Admin))]
    [HttpDelete("{companyId}/representatives/{representativeId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteRepresentative(
        [FromServices] IDeleteLegalRepresentativeUseCase useCase,
        [FromRoute] Guid companyId,
        [FromRoute] Guid representativeId)
    {
        await useCase.Execute(companyId, representativeId);
        return NoContent();
    }

}
