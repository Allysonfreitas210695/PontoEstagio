using PontoEstagio.Communication.Request;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.Cource;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Cource.Update;

public class UpdateCourceUseCase : IUpdateCourceUseCase
{
    private readonly ICourceUpdateOnlyRepository _courceUpdateOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCourceUseCase(ICourceUpdateOnlyRepository courceUpdateOnlyRepository, IUnitOfWork unitOfWork)
    {
        _courceUpdateOnlyRepository = courceUpdateOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid id, RequestRegisterCourceJson request)
    {
        Validate(request);

        var cource = await _courceUpdateOnlyRepository.GetByIdAsync(id);
        if (cource is null)
            throw new NotFoundException(ErrorMessages.CourseNotFound);

        cource.UpdateName(request.Name);
        cource.UpdateWorkloadHours(request.WorkloadHours);
        cource.UpdateUniversityId(request.UniversityId);

        _courceUpdateOnlyRepository.Update(cource);
        
        await _unitOfWork.CommitAsync();
    }

    private void Validate(RequestRegisterCourceJson request)
    {
        var result = new RegisterCourceValidator().Validate(request);
 
        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}