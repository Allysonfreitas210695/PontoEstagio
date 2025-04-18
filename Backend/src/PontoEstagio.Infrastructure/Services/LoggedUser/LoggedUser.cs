using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Infrastructure.Context;
using PontoEstagio.Infrastructure.Security.Tokens;

namespace PontoEstagio.Infrastructure.Services.LoggedUser;

public class LoggedUser : ILoggedUser
{
     private readonly PontoEstagioDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(PontoEstagioDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    } 

    public async Task<User> Get()
    {
        var token = _tokenProvider.TokenOnRequest();

        var tokenHandler = new JwtSecurityTokenHandler();

        var  jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var id = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

        return await _dbContext.Users
                                .AsNoTracking()
                                .FirstAsync(user => user.Id == Guid.Parse(id));
    }
}