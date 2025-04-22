using PontoEstagio.Communication.Request;
using PontoEstagio.Communication.Responses; 
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.Comapany;
using PontoEstagio.Exceptions.Exceptions;

namespace PontoEstagio.Application.UseCases.Company.Register;

public class RegisterCompanyUseCase : IRegisterCompanyUseCase
{
    private readonly ICompanyWriteOnlyRepository _companyWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    public RegisterCompanyUseCase(
        ICompanyWriteOnlyRepository companyWriteOnlyRepository,
        IUnitOfWork unitOfWork
    )
    {
        _companyWriteOnlyRepository = companyWriteOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseCompanyJson> Execute(RequestRegisterCompanytJson request)
    {
        Validate(request); 

        var company = new  Domain.Entities.Company(Guid.NewGuid(), request.Name, request.CNPJ, request.Email, request.Phone);

        await _companyWriteOnlyRepository.AddAsync(company);
        
        await _unitOfWork.CommitAsync();

        return new ResponseCompanyJson() {
            Id  = company.Id,
            Name = company.Name,
            CNPJ = company.CNPJ,
            Email = company.Email,
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
