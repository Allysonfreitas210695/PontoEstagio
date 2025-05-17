using PontoEstagio.Communication.Request;
using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.University;
using PontoEstagio.Domain.ValueObjects;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.University.Register;

public class RegisterUniversityUseCase : IRegisterUniversityUseCase
{
    private readonly IUniversityReadOnlyRepository _universityReadOnlyRepository;
    private readonly IUniversityWriteOnlyRepository _universityWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;


    public RegisterUniversityUseCase(
        IUniversityReadOnlyRepository universityReadOnlyRepository,
        IUniversityWriteOnlyRepository universityWriteOnlyRepository, 
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser
    )
    {
        _universityReadOnlyRepository = universityReadOnlyRepository;
        _universityWriteOnlyRepository = universityWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseUniversityJson> Execute(RequestRegisterUniversityJson request)
    {
        Validate(request);

        var user = await _loggedUser.Get();

        if (user is null)
            throw new NotFoundException(ErrorMessages.UserNotFound);

        if (user.Type != Domain.Enum.UserType.Admin)
            throw new ForbiddenException(ErrorMessages.UserNotAdmin);

        if(await _universityReadOnlyRepository.ExistsByEmailAsync(request.Email))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.EmailAlreadyRegistered });

        if(await _universityReadOnlyRepository.ExistsByCNPJAsync(request.CNPJ))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.CNPJAlreadyRegistered });

        var university = new Domain.Entities.University(
            Guid.NewGuid(),
            request.Name,
            request.Acronym,
            request.CNPJ,
            Email.Criar(request.Email),
            request.Phone,
            true,
            new Address(
                        request.Address.Street,
                        request.Address.Number,
                        request.Address.District,
                        request.Address.City,
                        request.Address.State,
                        request.Address.ZipCode,
                        request.Address.Complement
                    )                  
        );

        await _universityWriteOnlyRepository.AddAsync(university);

        await _unitOfWork.CommitAsync();

        return new ResponseUniversityJson() {
            Id = university.Id,
            Name = university.Name,
            Phone = university.Phone,
            Acronym = university.Acronym,
            Address = new ResponseAddressJson() {
                City = university.Address.City,
                Complement = university.Address.Complement,
                District = university.Address.District,
                Number = university.Address.Number,
                State = university.Address.State,
                Street = university.Address.Street
            },
            CNPJ = university.CNPJ,
            Email = university.Email.Endereco,
            IsActive = university.IsActive,
        };
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