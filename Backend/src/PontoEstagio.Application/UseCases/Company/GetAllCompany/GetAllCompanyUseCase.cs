using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Repositories.Comapany;

namespace PontoEstagio.Application.UseCases.Company.GetAllCompany;

public class GetAllCompanyUseCase : IGetAllCompanyUseCase
{
    private readonly ICompanyReadOnlyRepository _companyReadOnlyRepository;

    public GetAllCompanyUseCase(ICompanyReadOnlyRepository companyReadOnlyRepository)
    {
        _companyReadOnlyRepository = companyReadOnlyRepository;
    }

    public async Task<List<ResponseCompanyJson>> Execute()
    {
       var _companies = await _companyReadOnlyRepository.GetAllCompanyAsync();

       return _companies.Select(company => new ResponseCompanyJson
       {
           Id = company.Id,
           Name = company.Name,
           CNPJ = company.CNPJ,
           Phone = company.Phone,
           Email = company.Email.Endereco,
           CreatedAt = company.CreatedAt,
           Address = new ResponseAddressJson()
           {
                City = company.Address.City,
                Complement = company.Address.Complement,
                District = company.Address.District,
                Number = company.Address.Number,
                State = company.Address.State,
                Street = company.Address.Street,
                ZipCode = company.Address.ZipCode
            },
        }).ToList();   
    }
}