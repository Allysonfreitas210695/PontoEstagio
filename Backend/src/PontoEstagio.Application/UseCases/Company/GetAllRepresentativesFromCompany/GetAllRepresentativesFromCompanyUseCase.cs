using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Repositories.LegalRepresentative;

namespace PontoEstagio.Application.UseCases.Company.GetAllRepresentativesFromCompany;

public class GetAllRepresentativesFromCompanyUseCase : IGetAllRepresentativesFromCompanyUseCase
{
    private readonly ILegalRepresentativeReadOnlyRepository _legalRepresentativeReadOnlyRepository;

    public GetAllRepresentativesFromCompanyUseCase(ILegalRepresentativeReadOnlyRepository legalRepresentativeReadOnlyRepository)
    {
        _legalRepresentativeReadOnlyRepository = legalRepresentativeReadOnlyRepository;
    }

    public async Task<List<ResponseLegalRepresentativeJson>> Execute(Guid companyId)
    {
        var legalRepresentatives = await _legalRepresentativeReadOnlyRepository.GetAllLegalRepresentativeAsync(companyId);
        return legalRepresentatives
            .Select(lr => new ResponseLegalRepresentativeJson
            {
                Id = lr.Id,
                FullName = lr.FullName,
                CPF = lr.CPF,
                Position = lr.Position,
                Email = lr.Email.Endereco,
                Company = new ResponseCompanyJson
                {
                    Id = lr.Company.Id,
                    Name = lr.Company.Name,
                    CNPJ = lr.Company.CNPJ,
                    Email = lr.Company.Email.Endereco,
                    Phone = lr.Company.Phone,
                    CreatedAt = lr.Company.CreatedAt,
                    IsActive = lr.Company.IsActive
                }
            }).ToList();
    }

}