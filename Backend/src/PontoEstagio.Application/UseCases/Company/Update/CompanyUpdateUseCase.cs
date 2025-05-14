 using PontoEstagio.Communication.Request;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.Comapany;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Company.Update;

public class CompanyUpdateUseCase : ICompanyUpdateUseCase
{
    private readonly ICompanyUpdateOnlyRepository _companyUpdateOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CompanyUpdateUseCase(ICompanyUpdateOnlyRepository companyUpdateOnlyRepository, IUnitOfWork unitOfWork)
    {
        _companyUpdateOnlyRepository = companyUpdateOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid id, RequestRegisterCompanytJson request)
    {
        Validate(request);
        
        var _company = await _companyUpdateOnlyRepository.GetByIdAsync(id);
        if (_company == null)
            throw new NotFoundException(ErrorMessages.Company_NotFound);

        _company.UpdateCNPJ(request.CNPJ);
        _company.UpdateName(request.Name);
        _company.UpdateEmail(request.Email);
        _company.UpdatePhone(request.Phone); 
        _company.UpdateAddress(new Domain.ValueObjects.Address(
            request.Address.Street,
            request.Address.Number,
            request.Address.District,
            request.Address.City,
            request.Address.State,
            request.Address.ZipCode,
            request.Address.Complement
        ));

        if(request.IsActive == false)
            _company.Deactivate();
        else
            _company.Activate();

        _companyUpdateOnlyRepository.Update(_company);

        await _unitOfWork.CommitAsync();
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