using System.Linq.Expressions;
using PontoEstagio.Communication.Request;
using PontoEstagio.Domain.Helpers;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.PasswordRecovery;
using PontoEstagio.Domain.Repositories.User;
using PontoEstagio.Domain.Security.Cryptography;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Auth.ResetPassword;

public class ResetPasswordUseCase : IResetPasswordUseCase
{
    private readonly IUserUpdateOnlyRepository _useUpdateOnlyRepository;
    private readonly IPasswordRecoveryUpdateOnlyRespository _passwordRecoveryUpdateOnlyRespository;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly IUnitOfWork _unitOfWork;

    public ResetPasswordUseCase(
        IUserUpdateOnlyRepository userUpdateOnlyRepository,
        IPasswordRecoveryUpdateOnlyRespository passwordRecoveryUpdateOnlyRespository,
        IPasswordEncrypter passwordEncrypter,
        IUnitOfWork unitOfWork
    )
    {
        _useUpdateOnlyRepository = userUpdateOnlyRepository;
        _passwordRecoveryUpdateOnlyRespository = passwordRecoveryUpdateOnlyRespository;
        _passwordEncrypter = passwordEncrypter;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(RequestResetPasswordJson request)
    {
        Validate(request);

        var user = await _useUpdateOnlyRepository.GetUserByEmailAsync(request.Email);
        if (user == null)
            throw new NotFoundException(ErrorMessages.UserNotFound);

        var recovery = await _passwordRecoveryUpdateOnlyRespository.GetPasswordRecoveryByCode(request.Code, user.Id);
        if (recovery is null)
            throw new NotFoundException(ErrorMessages.PasswordRecovery_Code_Invalid);

        recovery!.EnsureUsable();
        recovery!.MarkAsUsed();

        _passwordRecoveryUpdateOnlyRespository.Update(recovery);

        var _passwordHash = _passwordEncrypter.Encrypt(request.NewPassword);
        user.UpdatePassword(_passwordHash);

        _useUpdateOnlyRepository.Update(user);

        await _unitOfWork.CommitAsync();

    }

    private void Validate(RequestResetPasswordJson request)
    {
        var result = new ResetPasswordValidator().Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            if (errorMessages.Any())
                throw new ErrorOnValidationException(errorMessages);
        }
    }
}
