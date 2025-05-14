using PontoEstagio.Communication.Request;
using PontoEstagio.Domain.Helpers;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.EmailTemplate;
using PontoEstagio.Domain.Repositories.PasswordRecovery;
using PontoEstagio.Domain.Services.Email;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Auth.ForgotPassword;

public class ForgotPasswordUseCase : IForgotPasswordUseCase
{
    private readonly IUserReadOnlyRepository _useReadOnlyRepository; 
    private readonly IEmailTemplateReadOnlyRepository _emailTemplateReadOnlyRepository;
    private readonly IEmailService _emailService;
    private readonly IPasswordRecoveryWriteOnlyRespository _passwordRecoveryWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ForgotPasswordUseCase(
        IUserReadOnlyRepository userReadOnlyRepository,
        IEmailTemplateReadOnlyRepository emailTemplateReadOnlyRepository,
        IEmailService emailService,
        IPasswordRecoveryWriteOnlyRespository passwordRecoveryWriteOnlyRepository,
        IUnitOfWork unitOfWork
     )
    {
        _useReadOnlyRepository = userReadOnlyRepository; 
        _emailTemplateReadOnlyRepository = emailTemplateReadOnlyRepository;
        _emailService = emailService;
        _passwordRecoveryWriteOnlyRepository = passwordRecoveryWriteOnlyRepository; 
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(RequestForgotPasswordJson request)
    {
        var user = await _useReadOnlyRepository.GetUserByEmailAsync(request.Email);
        if (user == null)
            throw new NotFoundException(ErrorMessages.UserNotFound);

        var _emailPasswordReset = await _emailTemplateReadOnlyRepository.GetEmailTemplatesByTitle("Redefinição de Senha");
        if (_emailPasswordReset is null)
            throw new NotFoundException(ErrorMessages.EmailTemplatePasswordResetNotFound);

        var _codeGenerate = CodeGeneratorHelper.Generate(6);

        await _passwordRecoveryWriteOnlyRepository.AddAsync(new Domain.Entities.PasswordRecovery(Guid.NewGuid(), user.Id, _codeGenerate));
        await _unitOfWork.CommitAsync();

        _emailPasswordReset.Body = _emailPasswordReset.Body.Replace("{{UserName}}", user.Name);
        _emailPasswordReset.Body = _emailPasswordReset.Body.Replace("{{VerificationCode}}", _codeGenerate);
        
        await _emailService.SendEmailAsync(user.Email.Endereco, _emailPasswordReset.Subject, _emailPasswordReset.Body); 
    }
}

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
