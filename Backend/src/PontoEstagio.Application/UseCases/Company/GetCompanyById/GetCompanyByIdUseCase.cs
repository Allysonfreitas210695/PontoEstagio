using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Repositories.Comapany;
using PontoEstagio.Exceptions.Exceptions;

namespace PontoEstagio.Application.UseCases.Company.GetCompanyById;

public class GetCompanyByIdUseCase : IGetCompanyByIdUseCase
{
    private readonly ICompanyReadOnlyRepository _companyReadOnlyRepository;

    public GetCompanyByIdUseCase(ICompanyReadOnlyRepository companyReadOnlyRepository)
    {
        _companyReadOnlyRepository = companyReadOnlyRepository;
    }
    public async Task<ResponseCompanyJson> Execute(Guid id)
    {
        var company = await _companyReadOnlyRepository.GetByIdAsync(id);    
        if(company == null)
            throw new NotFoundException("Company not found");

        return new ResponseCompanyJson() {
            Id  = company.Id,
            Name = company.Name,
            CNPJ = company.CNPJ,
            Email = company.Email,
            Phone = company.Phone,
            CreatedAt = company.CreatedAt
        };
    }
}