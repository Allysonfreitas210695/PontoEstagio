using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Repositories.LegalRepresentative;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Company.GetLegalRepresentativeById;
public class GetLegalRepresentativeByIdUseCase : IGetLegalRepresentativeByIdUseCase
{
    private readonly ILegalRepresentativeReadOnlyRepository _legalRepresentativeReadOnlyRepository;

    public GetLegalRepresentativeByIdUseCase(ILegalRepresentativeReadOnlyRepository legalRepresentativeReadOnlyRepository)
    {
        _legalRepresentativeReadOnlyRepository = legalRepresentativeReadOnlyRepository;
    }
    public async Task<ResponseLegalRepresentativeJson> Execute(Guid companyId, Guid representativeId)
    {
        var legalRepresentatives = await _legalRepresentativeReadOnlyRepository.GetByIdAsync(companyId, representativeId);
        if (legalRepresentatives == null)
            throw new NotFoundException(ErrorMessages.LegalRepresentativeNotFound);

        return new ResponseLegalRepresentativeJson()
        {
            Id = legalRepresentatives.Id,
            FullName = legalRepresentatives.FullName,
            CPF = legalRepresentatives.CPF,
            Position = legalRepresentatives.Position,
            Email = legalRepresentatives.Email.Endereco,
            Company = new ResponseCompanyJson
            {
                Id = legalRepresentatives.Company.Id,
                Name = legalRepresentatives.Company.Name,
                CNPJ = legalRepresentatives.Company.CNPJ,
                Email = legalRepresentatives.Company.Email.Endereco,
                Phone = legalRepresentatives.Company.Phone,
                CreatedAt = legalRepresentatives.Company.CreatedAt,
                IsActive = legalRepresentatives.Company.IsActive
            }
        };
    }
}
