using PontoEstagio.Communication.Request;
using PontoEstagio.Domain.Helpers;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.EmailTemplate;
using PontoEstagio.Domain.Repositories.PasswordRecovery;
using PontoEstagio.Domain.Repositories.User;
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
        
        await _emailService.SendEmailAsync("alisonfr83@gmail.com", _emailPasswordReset.Subject, _emailPasswordReset.Body); 
    }
}
