using FluentValidation.Results;
using PontoEstagio.Communication.Request;
using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.User;
using PontoEstagio.Domain.Security.Cryptography;
using PontoEstagio.Domain.Security.Token;
using PontoEstagio.Domain.ValueObjects;
using PontoEstagio.Exceptions.Exceptions;

namespace PontoEstagio.Application.UseCases.Users.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenGenerateAccessToken _tokenGenerateAccessToken;
    private readonly ITokenRefreshToken _tokenRefreshToken;
    private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;

    public RegisterUserUseCase(
        IUserReadOnlyRepository userReadOnlyRepository,
        IUserWriteOnlyRepository userWriteOnlyRepository,
        IPasswordEncrypter passwordEncrypter,
        IUnitOfWork unitOfWork,
        ITokenGenerateAccessToken tokenGenerateAccessToken,
        ITokenRefreshToken tokenRefreshToken,
        IUserRefreshTokenRepository userRefreshTokenRepository
    )
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _passwordEncrypter = passwordEncrypter;
        _unitOfWork = unitOfWork;
        _tokenGenerateAccessToken = tokenGenerateAccessToken;
        _tokenRefreshToken = tokenRefreshToken;
        _userRefreshTokenRepository = userRefreshTokenRepository;
    }

    public async Task<ResponseLoggedUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var _passwordHash = _passwordEncrypter.Encrypt(request.Password);

        var user = new User(
            Guid.NewGuid(),
            request.Name, 
            Email.Criar(request.Email), 
            (UserType)request.Type, 
            _passwordHash,
            true
        );

        await _userWriteOnlyRepository.AddAsync(user);

        await _unitOfWork.CommitAsync();

        var newAccessToken = _tokenGenerateAccessToken.GenerateAccessToken(user);
        var newRefreshToken = _tokenRefreshToken.GenerateRefreshToken();

        await _userRefreshTokenRepository.InsertAsync(new UserRefreshToken
        {
            UserId = user.Id,
            Token = newRefreshToken,
            ExpirationDate = DateTime.UtcNow.AddDays(2)
        });

        return new ResponseLoggedUserJson()
        {
            Name = user.Name,
            Type = user.Type.ToString(),
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var result = new RegisterUserValidator().Validate(request);

        var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmailAsync(request.Email);

        if (emailExist)
            result.Errors.Add(new ValidationFailure(string.Empty, "Email is already in use."));

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}