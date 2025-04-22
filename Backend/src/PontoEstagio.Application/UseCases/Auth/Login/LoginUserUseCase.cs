using PontoEstagio.Communication.Request;
using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.User;
using PontoEstagio.Domain.Security.Cryptography;
using PontoEstagio.Domain.Security.Token;
using PontoEstagio.Exceptions.Exceptions;

namespace PontoEstagio.Application.UseCases.Login.DoLogin;

public class LoginUserUseCase : ILoginUserUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly ITokenGenerateAccessToken _tokenGenerateAccessToken; 
    private readonly ITokenRefreshToken _tokenRefreshToken; 
    private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LoginUserUseCase(
        IUserReadOnlyRepository userReadOnlyRepository,
        IPasswordEncrypter passwordEncrypter,
        ITokenGenerateAccessToken tokenGenerateAccessToken,
        ITokenRefreshToken tokenRefreshToken,
        IUserRefreshTokenRepository userRefreshTokenRepository,
        IUnitOfWork unitOfWork
    )
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _passwordEncrypter = passwordEncrypter;
        _tokenGenerateAccessToken = tokenGenerateAccessToken;
        _tokenRefreshToken = tokenRefreshToken;
        _userRefreshTokenRepository = userRefreshTokenRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseLoggedUserJson> Execute(RequestLoginUserJson request)
    {
        var result = new LoginValidator().Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }

        var _user = await _userReadOnlyRepository.GetUserByEmailAsync(request.Email);

        if (_user is null)
            throw new InvalidLoginException();

        var isValidPassword = _passwordEncrypter.Verify(request.Password, _user.Password);
        if (isValidPassword == false)
            throw new InvalidLoginException();

        var accessToken = _tokenGenerateAccessToken.GenerateAccessToken(_user);
        var refreshToken = _tokenRefreshToken.GenerateRefreshToken();

        var _userRefreshToken = new Domain.Entities.UserRefreshToken(
            Guid.NewGuid(),
            _user.Id,
            refreshToken,
            DateTime.UtcNow.AddDays(2)
        );
        await _userRefreshTokenRepository.InsertAsync(_userRefreshToken);

        await _unitOfWork.CommitAsync();

        return new ResponseLoggedUserJson()
        {
            Name = _user.Name,
            Type = _user.Type.ToString(),
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}
