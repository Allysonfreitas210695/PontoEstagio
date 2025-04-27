using PontoEstagio.Communication.Request;
using PontoEstagio.Domain.Helpers;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.User;
using PontoEstagio.Domain.Security.Cryptography;
using PontoEstagio.Domain.Services.Email;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Auth.ForgotPassword;

public class ForgotPasswordUseCase : IForgotPasswordUseCase
{
    private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;

    public ForgotPasswordUseCase(
        IUserUpdateOnlyRepository userUpdateOnlyRepository,
        IPasswordEncrypter passwordEncrypter,
        IUnitOfWork unitOfWork,
        IEmailService emailService
     )
    {
        _userUpdateOnlyRepository = userUpdateOnlyRepository;
        _passwordEncrypter = passwordEncrypter;
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }

    public async Task Execute(RequestForgotPasswordJson request)
    {
        var user = await _userUpdateOnlyRepository.GetUserByEmailAsync(request.Email);
        if (user == null)
            throw new NotFoundException(ErrorMessages.UserNotFound);

        var _password = PasswordHelper.GenerateRandomPassword();
        var _passwordHash = _passwordEncrypter.Encrypt(_password);

        user.UpdatePassword(_passwordHash);
            
        _userUpdateOnlyRepository.Update(user);

        var emailSubject = "Sua senha temporária";
        var emailBody = $@"
                    <html>
                        <body>
                            <h2>Olá, {user.Name}!</h2>
                            <p>Recebemos uma solicitação para redefinir sua senha.</p>
                            <p>Sua nova senha temporária é: <strong>{_password}</strong></p>
                            <p>Por favor, use essa senha para acessar sua conta.</p>
                            <p>Recomendamos que você altere sua senha assim que fizer o login.</p>
                            <p>Se você não solicitou a redefinição de senha, por favor, entre em contato conosco imediatamente.</p>
                            <br />
                            <p>Atenciosamente,<br />Equipe Ponto Estágio</p>
                        </body>
                    </html>
                ";

        await _emailService.SendEmailAsync(user.Email.Endereco, emailSubject, emailBody);


        await _unitOfWork.CommitAsync();
    }
}
