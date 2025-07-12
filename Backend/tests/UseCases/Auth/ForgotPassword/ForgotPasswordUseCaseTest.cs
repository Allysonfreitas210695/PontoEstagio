using CommonTestUltilities.Entities;
using CommonTestUltilities.Repositories;
using CommonTestUltilities.Security;
using CommonTestUltilities.Services;
using FluentAssertions;
using PontoEstagio.Application.UseCases.Auth.ForgotPassword;
using PontoEstagio.Communication.Request;
using PontoEstagio.Domain.ValueObjects;

namespace UseCases.Auth.ForgotPassword;

public class ForgotPasswordUseCaseTest
{
    [Fact]
    public async Task Execute_WithValidEmail_ShouldSendEmailWithCode()
    {
        // Arrange
        var email = "user@email.com";
        var user = UserBuilder.Build(email: Email.Criar(email));
       var emailTemplate = EmailTemplateBuilder.Build(
            title: "Redefinição de Senha",
            subject: "Assunto",
            body: "Olá {{UserName}}, seu código é: {{VerificationCode}}"
        );


        var request = new RequestForgotPasswordJson { Email = email };

        var userRepoMock = new UserReadOnlyRepositoryMockBuilder()
            .GetUserByEmailAsync(user)
            .Build();

        var emailTemplateRepoMock = new EmailTemplateReadOnlyRepositoryMockBuilder()
            .SetupGetTemplateByTitle("Redefinição de Senha", emailTemplate)
            .Build();

        var emailServiceMock = new EmailServiceMockBuilder()
            .SetupSendEmail()
            .Build();

        var passwordRecoveryRepoMock = new PasswordRecoveryWriteOnlyRepositoryMockBuilder()
            .SetupAddAsync()
            .Build();

        var unitOfWorkMock = new UnitOfWorkMockBuilder()
            .SetupCommitAsync()
            .Build();

        var useCase = new ForgotPasswordUseCase(
            userRepoMock,
            emailTemplateRepoMock,
            emailServiceMock,
            passwordRecoveryRepoMock,
            unitOfWorkMock
        );

        // Act
        Func<Task> act = async () => await useCase.Execute(request);

        // Assert
        await act.Should().NotThrowAsync();
    }
}
