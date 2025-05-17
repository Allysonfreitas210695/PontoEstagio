using PontoEstagio.Communication.Request;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.University;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.University.Update;

public class UpdateUniversityUseCase : IUpdateUniversityUseCase
{
    private readonly IUniversityUpdateOnlyRepository _universityUpdateOnlyRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUniversityUseCase(
        IUniversityUpdateOnlyRepository universityUpdateOnlyRepository,
        ILoggedUser loggedUser, 
        IUnitOfWork unitOfWork
    )
    {
        _universityUpdateOnlyRepository = universityUpdateOnlyRepository;
        _loggedUser = loggedUser;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid universityId, RequestRegisterUniversityJson request){
        Validate(request);

        var currentUser = await _loggedUser.Get();
        if (currentUser is null)
            throw new NotFoundException(ErrorMessages.UserNotFound);

        if (currentUser.Type != Domain.Enum.UserType.Admin)
            throw new ForbiddenException(ErrorMessages.UserNotAdmin);

        var university = await _universityUpdateOnlyRepository.GetUniversityByIdAsync(universityId);
        if (university is null)
            throw new NotFoundException("");

        if (await _universityUpdateOnlyRepository.ExistsByEmailAsync(universityId, request.Email))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.EmailAlreadyRegistered });

        if (await _universityUpdateOnlyRepository.ExistsByCNPJAsync(universityId, request.CNPJ))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.CNPJAlreadyRegistered });

        university.UpdateName(request.Name);
        university.UpdateAcronym(request.Acronym);
        university.UpdateCNPJ(request.CNPJ);
        university.UpdatePhone(request.Phone);
        university.UpdateEmail(request.Email);
        university.UpdateAddress(new Domain.ValueObjects.Address(
            request.Address.Street,
            request.Address.Number,
            request.Address.District,
            request.Address.City,
            request.Address.State,
            request.Address.ZipCode,
            request.Address.Complement
        ));

        _universityUpdateOnlyRepository.Update(university);

        await _unitOfWork.CommitAsync();
    }

    private void Validate(RequestRegisterUniversityJson request)
    {
        var result = new RegisterUniversityValidator().Validate(request);
 
        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}