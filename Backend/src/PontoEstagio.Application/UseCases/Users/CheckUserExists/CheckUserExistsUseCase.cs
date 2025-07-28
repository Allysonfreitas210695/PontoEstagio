using FluentValidation.Results;
using PontoEstagio.Communication.Request;
using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.EmailTemplate;
using PontoEstagio.Domain.Repositories.VerificationCodeUniversity;
using PontoEstagio.Domain.Services.Email;
using PontoEstagio.Domain.ValueObjects;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Users.CheckUserExists;

public class CheckUserExistsUseCase : ICheckUserExistsUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IVerificationCodeUniversityOnlyWriteRepository _verificationCodeWriteRepository;
    private readonly IVerificationCodeUniversityOnlyReadRepository _verificationCodeOnlyRepository;
    private readonly IEmailTemplateReadOnlyRepository _emailTemplateReadOnlyRepository;
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;

    public CheckUserExistsUseCase(
        IUserReadOnlyRepository userReadOnlyRepository,
        IVerificationCodeUniversityOnlyWriteRepository verificationCodeWriteRepository,
        IVerificationCodeUniversityOnlyReadRepository verificationCodeUniversityOnlyReadRepository,
        IEmailTemplateReadOnlyRepository emailTemplateReadOnlyRepository,
        IEmailService emailService,
        IUnitOfWork unitOfWork)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _verificationCodeWriteRepository = verificationCodeWriteRepository;
        _verificationCodeOnlyRepository = verificationCodeUniversityOnlyReadRepository;
        _emailTemplateReadOnlyRepository = emailTemplateReadOnlyRepository;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
    }

    private static string GenerateCode()
    {
        var random = new Random();
        return random.Next(100000, 999999).ToString();
    }

    public async Task<ResponseCheckUserUserJson> Execute(RequestCheckUserExistsUserJson request)
    {
        Validate(request);

        var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmailAsync(request.Email);
        if (emailExist)
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.EmailAlreadyInUse });

        if (request.Type == Communication.Enum.UserType.Coordinator)
        {
            var codeExists = await _verificationCodeOnlyRepository.ExistsActiveCodeForEmailAsync(request.Email);
            if (!codeExists)
            {
                var code = GenerateCode();

                var verificationCode = new VerificationCodeUniversity(
                    id: Guid.NewGuid(),
                    email: Email.Criar(request.Email),
                    code: code,
                    expiration: DateTime.UtcNow.AddMinutes(3)
                );

                await _verificationCodeWriteRepository.AddAsync(verificationCode);

                var _emailVerificationCode = await _emailTemplateReadOnlyRepository.GetEmailTemplatesByTitle("Cadastro de Coordenação");
                if (_emailVerificationCode is null)
                    throw new NotFoundException(ErrorMessages.EmailTemplatePasswordResetNotFound);

                _emailVerificationCode.Body = _emailVerificationCode.Body.Replace("{{VerificationCode}}", code);
                await _emailService.SendEmailAsync(request.Email, _emailVerificationCode.Subject, _emailVerificationCode.Body);

                await _unitOfWork.CommitAsync();
            }else
                throw new ErrorOnValidationException(new List<string> { ErrorMessages.VerificationCodeAlreadyExists });

        }

        return new ResponseCheckUserUserJson
        {
            Email = request.Email,
            Type = request.Type,
            Password = request.Password
        };
    }

    private void Validate(RequestCheckUserExistsUserJson request)
    {
        var result = new CheckUserExistsValidator().Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
