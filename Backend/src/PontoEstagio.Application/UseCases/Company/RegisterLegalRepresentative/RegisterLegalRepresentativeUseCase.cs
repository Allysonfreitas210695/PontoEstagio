using PontoEstagio.Communication.Request;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.Comapany;
using PontoEstagio.Domain.Repositories.LegalRepresentative;
using PontoEstagio.Domain.ValueObjects;
using PontoEstagio.Exceptions.Exceptions;

namespace PontoEstagio.Application.UseCases.Company.RegisterLegalRepresentative;

public class RegisterLegalRepresentativeUseCase : IRegisterLegalRepresentativeUseCase
{
    private readonly ILegalRepresentativeReadOnlyRepository _legalRepresentativeReadOnlyRepository;
    private readonly ILegalRepresentativeWriteOnlyRepository _legalRepresentativeWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterLegalRepresentativeUseCase(
        ILegalRepresentativeReadOnlyRepository legalRepresentativeReadOnlyRepository,
        ILegalRepresentativeWriteOnlyRepository legalRepresentativeWriteOnlyRepository,
        IUnitOfWork unitOfWork
    )
    {
        _legalRepresentativeReadOnlyRepository = legalRepresentativeReadOnlyRepository;
        _legalRepresentativeWriteOnlyRepository = legalRepresentativeWriteOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(RequestRegisterLegalRepresentativeJson request)
    {
        Validate(request);

        if (await _legalRepresentativeReadOnlyRepository.ExistsByEmailAsync(request.Email))
            throw new ErrorOnValidationException(new List<string> { "Email já cadastrado." });

        if (await _legalRepresentativeReadOnlyRepository.ExistsByCPFAsync(request.CPF))
            throw new ErrorOnValidationException(new List<string> { "CPF já cadastrado." });

        var legalRepresentative = new Domain.Entities.LegalRepresentative(
            Guid.NewGuid(),
            request.FullName,
            request.CPF,
            request.Position,
            Email.Criar(request.Email),
            request.CompanyId
        );
        
        await _legalRepresentativeWriteOnlyRepository.AddAsync(legalRepresentative);
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