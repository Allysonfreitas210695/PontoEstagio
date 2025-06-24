using PontoEstagio.Communication.Request;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.Cource;
using PontoEstagio.Domain.Repositories.University;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Cource.Register;

public class RegisterCourceUseCase : IRegisterCourceUseCase
{   
    private readonly ICourceWriteOnlyRepository _courceWriteOnlyRepository;
    private readonly IUniversityReadOnlyRepository _universityReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCourceUseCase(
        ICourceWriteOnlyRepository courceWriteOnlyRepository, 
        IUniversityReadOnlyRepository universityReadOnlyRepository,
        IUnitOfWork unitOfWork
    )
    {
        _courceWriteOnlyRepository = courceWriteOnlyRepository;
        _universityReadOnlyRepository = universityReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(RequestRegisterCourceJson request)
    {
        Validate(request);

        var university = await _universityReadOnlyRepository.GetUniversityByIdAsync(request.UniversityId);
        if (university is null)
            throw new NotFoundException("");

        var cource = new Domain.Entities.Course(
            Guid.NewGuid(),
            request.Name,
            request.WorkloadHours,
            request.UniversityId
        );

        await _courceWriteOnlyRepository.AddAsync(cource);

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