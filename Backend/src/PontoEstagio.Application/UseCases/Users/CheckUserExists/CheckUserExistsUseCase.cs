using FluentValidation.Results;
using PontoEstagio.Communication.Request;
using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Users.CheckUserExists;

public class CheckUserExistsUseCase : ICheckUserExistsUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public CheckUserExistsUseCase(IUserReadOnlyRepository userReadOnlyRepository)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
    }

    public async Task<ResponseCheckUserUserJson> Execute(RequestCheckUserExistsUserJson request)
    {
        Validate(request);

        var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmailAsync(request.Email);
        if (emailExist)
            throw new ErrorOnValidationException(new List<string>{ ErrorMessages.EmailAlreadyInUse } );

        return new ResponseCheckUserUserJson
        {
            Email = request.Email,
            Type = request.Type,
            Password = request.Password
        };
    }

    private void Validate(RequestCheckUserExistsUserJson request)
    {
        var result = new CheckUserExistsValidator().Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}