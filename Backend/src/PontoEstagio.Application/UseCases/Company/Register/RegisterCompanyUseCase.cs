using PontoEstagio.Communication.Request;
using PontoEstagio.Communication.Responses; 
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.Comapany;
using PontoEstagio.Domain.Services.Email;
using PontoEstagio.Domain.ValueObjects;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Company.Register;

public class RegisterCompanyUseCase : IRegisterCompanyUseCase
{
    private readonly ICompanyWriteOnlyRepository _companyWriteOnlyRepository;
    private readonly ICompanyReadOnlyRepository _companyReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    public RegisterCompanyUseCase(
        ICompanyWriteOnlyRepository companyWriteOnlyRepository,
        ICompanyReadOnlyRepository companyReadOnlyRepository,
        IUnitOfWork unitOfWork,
        IEmailService emailService
    )
    {
        _companyWriteOnlyRepository = companyWriteOnlyRepository;
        _companyReadOnlyRepository = companyReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseCompanyJson> Execute(RequestRegisterCompanytJson request)
    {
        Validate(request); 

        if(await _companyReadOnlyRepository.ExistsByEmailAsync(request.Email))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.EmailAlreadyRegistered });

        if(await _companyReadOnlyRepository.ExistsByCNPJAsync(request.CNPJ))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.CNPJAlreadyRegistered });
        
        var company = new  Domain.Entities.Company(
                                            Guid.NewGuid(), 
                                            request.Name, 
                                            request.CNPJ, 
                                            request.Phone,
                                            Email.Criar(request.Email)
                                        );
        await _companyWriteOnlyRepository.AddAsync(company);
                
        await _unitOfWork.CommitAsync();

        return new ResponseCompanyJson() {
            Id  = company.Id,
            Name = company.Name,
            CNPJ = company.CNPJ,
            Email = company.Email.Endereco,
            Phone = company.Phone,
            CreatedAt = company.CreatedAt
        };
    }

    private void Validate(RequestRegisterCompanytJson request)
    {
        var result = new RegisterCompanyValidator().Validate(request);
 
        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
