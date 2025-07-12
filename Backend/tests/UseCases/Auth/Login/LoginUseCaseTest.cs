using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTestUltilities.Entities;
using CommonTestUltilities.Repositories;
using CommonTestUltilities.Security;
using FluentAssertions;
using PontoEstagio.Application.UseCases.Login.DoLogin;
using PontoEstagio.Communication.Request;
using PontoEstagio.Domain.ValueObjects;

namespace UseCases.Auth.Login;

public class LoginUseCaseTest
{
    [Fact]
    public async Task Execute_WithValidCredentials_ShouldReturnLoggedUser()
    {
        var request = new RequestLoginUserJson
        {
            Email = "teste@email.com",
            Password = "Senha123@"
        };

        var user = UserBuilder.Build(email:Email.Criar(request.Email), password: "Senha123@");

        var userRepoMock = new UserReadOnlyRepositoryMockBuilder()
            .GetUserByEmailAsync(user)
            .Build();

        var passwordEncrypterMock = new PasswordEncrypterMockBuilder()
            .Verify(request.Password, user.Password, true)
            .Build();

        var accessTokenMock = new TokenGenerateAccessTokenMockBuilder()
            .SetupGenerateAccessToken(user, "access_token")
            .Build();

        var refreshTokenMock = new TokenRefreshTokenMockBuilder()
            .SetupGenerateRefreshToken("refresh_token")
            .Build();

        var refreshTokenRepoMock = new UserRefreshTokenRepositoryMockBuilder()
            .SetupInsertAsync()
            .Build();

        var unitOfWorkMock = new UnitOfWorkMockBuilder()
            .SetupCommitAsync()
            .Build();

        var useCase = new LoginUserUseCase(
            userRepoMock,
            passwordEncrypterMock,
            accessTokenMock,
            refreshTokenMock,
            refreshTokenRepoMock,
            unitOfWorkMock
        );

        // Act
        var result = await useCase.Execute(request);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(user.Name);
        result.Type.Should().Be(user.Type.ToString());
        result.AccessToken.Should().Be("access_token");
        result.RefreshToken.Should().Be("refresh_token");
    }
}
