using System;
using System.Threading.Tasks;
using CommonTestUltilities.Entities;
using CommonTestUltilities.Repositories;
using CommonTestUltilities.Security;
using FluentAssertions;
using PontoEstagio.Application.UseCases.Auth.ResetPassword;
using PontoEstagio.Communication.Request;
using PontoEstagio.Domain.Entities;
using Xunit;

namespace UseCases.Auth.ResetPassword;

public class ResetPasswordUseCaseTest
{
    [Fact]
    public async Task Execute_WithValidRequest_ShouldResetPasswordSuccessfully()
    {
        // Arrange
        var newPassword = "NovaSenha123@";
        var encryptedPassword = "hashed_password";

        var user = UserBuilder.Build();
        var recovery = PasswordRecoveryBuilder.Build(userId: user.Id);

        var request = new RequestResetPasswordJson
        {
            Code = recovery.Code,
            NewPassword = newPassword
        };

        var passwordRecoveryRepoMock = new PasswordRecoveryUpdateOnlyRepositoryMockBuilder()
            .SetupGetByCode(recovery)
            .SetupUpdate()
            .Build();

        var userUpdateRepoMock = new UserUpdateOnlyRepositoryMockBuilder()
            .SetupGetUserByIdAsync(user)
            .SetupUpdate()
            .Build();

        var passwordEncrypterMock = new PasswordEncrypterMockBuilder()
            .Encrypt(newPassword, encryptedPassword)
            .Build();

        var unitOfWorkMock = new UnitOfWorkMockBuilder()
            .SetupCommitAsync()
            .Build();

        var useCase = new ResetPasswordUseCase(
            userUpdateRepoMock,
            passwordRecoveryRepoMock,
            passwordEncrypterMock,
            unitOfWorkMock
        );

        // Act
        Func<Task> act = async () => await useCase.Execute(request);

        // Assert
        await act.Should().NotThrowAsync();
    }
}
