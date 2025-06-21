using PontoEstagio.Communication.Request;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.LegalRepresentative;
using PontoEstagio.Exceptions.Exceptions;

namespace PontoEstagio.Application.UseCases.Company.UpdateLegalRepresentative;

public class UpdateLegalRepresentativeUseCase : IUpdateLegalRepresentativeUseCase
{
    private readonly ILegalRepresentativeUpdateOnlyRepository _legalRepresentativeUpdateOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLegalRepresentativeUseCase(ILegalRepresentativeUpdateOnlyRepository legalRepresentativeUpdateOnlyRepository, IUnitOfWork unitOfWork)
    {
        _legalRepresentativeUpdateOnlyRepository = legalRepresentativeUpdateOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid representativeId, RequestRegisterLegalRepresentativeJson request)
    {
        Validate(request);

        var legalRepresentative = await _legalRepresentativeUpdateOnlyRepository.GetByIdAsync(request.CompanyId, representativeId);
        if (legalRepresentative is null)
            throw new NotFoundException("Representante legal não encontrado.");

        legalRepresentative.UpdateFullName(request.FullName);
        legalRepresentative.UpdateCPF(request.CPF);
        legalRepresentative.UpdatePosition(request.Position);
        legalRepresentative.UpdateEmail(request.Email);

        _legalRepresentativeUpdateOnlyRepository.Update(legalRepresentative);
        await _unitOfWork.CommitAsync();
    }

    private static void Validate(RequestRegisterLegalRepresentativeJson request)
    {
        var result = new RegisterLegalRepresentativeValidator().Validate(request);
 
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}