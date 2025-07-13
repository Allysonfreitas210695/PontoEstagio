using CommonTestUltilities.Entities;
using CommonTestUltilities.Repositories;
using CommonTestUltilities.Security;
using FluentAssertions;
using PontoEstagio.Application.UseCases.Auth.Refresh;
using PontoEstagio.Communication.Request;

namespace UseCases.Auth.Refresh;

public class RefreshTokenUseCaseTest
{
    [Fact]
    public async Task Execute_WithValidRefreshToken_ShouldReturnNewTokens()
    {
        // Arrange
        var request = new RequestRefreshTokenJson
        {
            RefreshToken = "valid_refresh_token"
        };

        var user = UserBuilder.Build();
        var userId = user.Id;

        var accessToken = "new_access_token";
        var newRefreshToken = "new_refresh_token";

        var tokenAccessMock = new TokenGenerateAccessTokenMockBuilder()
            .SetupGenerateAccessToken(user, accessToken)
            .Build();

        var tokenRefreshMock = new TokenRefreshTokenMockBuilder()
            .SetupValidateRefreshToken(request.RefreshToken, userId)
            .SetupGenerateRefreshToken(newRefreshToken)
            .Build();

        var userRepoMock = new UserReadOnlyRepositoryMockBuilder()
            .GetUserByIdAsync(user)
            .Build();

        var refreshTokenRepoMock = new UserRefreshTokenRepositoryMockBuilder()
            .SetupInsertAsync()
            .Build();

        var useCase = new RefreshTokenUseCase(
            tokenAccessMock,
            tokenRefreshMock,
            userRepoMock,
            refreshTokenRepoMock
        );

        // Act
        var result = await useCase.Execute(request);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(user.Name);
        result.Type.Should().Be(user.Type.ToString());
        result.AccessToken.Should().Be(accessToken);
        result.RefreshToken.Should().Be(newRefreshToken);
    }
}
