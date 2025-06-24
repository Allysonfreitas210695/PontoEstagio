using PontoEstagio.Communication.Request;
using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.User;
using PontoEstagio.Domain.Security.Token;
using PontoEstagio.Exceptions.Exceptions;

namespace PontoEstagio.Application.UseCases.Auth.Refresh;

public class RefreshTokenUseCase : IRefreshTokenUseCase
{
    private readonly ITokenGenerateAccessToken _tokenGenerateAccessToken;
    private readonly ITokenRefreshToken _tokenRefreshToken;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;

    public RefreshTokenUseCase(
        ITokenGenerateAccessToken tokenGenerateAccessToken,
        ITokenRefreshToken tokenRefreshToken ,
        IUserReadOnlyRepository userReadOnlyRepository,
        IUserRefreshTokenRepository userRefreshTokenRepository
    )
    {
        _tokenGenerateAccessToken = tokenGenerateAccessToken;
        _tokenRefreshToken = tokenRefreshToken;
        _userReadOnlyRepository = userReadOnlyRepository;
        _userRefreshTokenRepository = userRefreshTokenRepository;
    }

    public async Task<ResponseLoggedUserJson> Execute(RequestRefreshTokenJson request)
    {
         var userId = _tokenRefreshToken.ValidateRefreshToken(request.RefreshToken);

        if (userId == null)
            throw new InvalidLoginException();

        var user = await _userReadOnlyRepository.GetUserByIdAsync(userId.Value);
        if (user == null)
            throw new InvalidLoginException();

        var newAccessToken = _tokenGenerateAccessToken.GenerateAccessToken(user);
        var newRefreshToken = _tokenRefreshToken.GenerateRefreshToken();

        var _userRefreshToken = new UserRefreshToken(
            Guid.NewGuid(),
            user.Id,
            newRefreshToken,
            DateTime.UtcNow.AddDays(2)
        );
        await _userRefreshTokenRepository.InsertAsync(_userRefreshToken);

        return new ResponseLoggedUserJson
        {
            Name = user.Name,
            Type = user.Type.ToString(),
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }
}