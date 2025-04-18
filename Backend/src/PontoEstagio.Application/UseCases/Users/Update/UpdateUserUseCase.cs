﻿using PontoEstagio.Communication.Request; 
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.User;
using PontoEstagio.Domain.Security.Cryptography;
using PontoEstagio.Domain.ValueObjects;
using PontoEstagio.Exceptions.Exceptions;

namespace PontoEstagio.Application.UseCases.Users.Update;
public class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserUseCase(
        IUserUpdateOnlyRepository userUpdateOnlyRepository, 
        IUnitOfWork unitOfWork,
        IPasswordEncrypter passwordEncrypter
    )
    {
        _userUpdateOnlyRepository = userUpdateOnlyRepository;
        _passwordEncrypter = passwordEncrypter;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid id, RequestRegisterUserJson request)
    {
        Validate(request);

        var _user = await _userUpdateOnlyRepository.GetUserByIdAsync(id);

        if (_user is null)
            throw new NotFoundException("User is not exists.");

        string passwordHash = string.IsNullOrWhiteSpace(request.Password)
            ? _user.Password
            : _passwordEncrypter.Encrypt(request.Password);

        _user.Name = request.Name;
        _user.Password = passwordHash;
        _user.Email = Email.Criar(request.Email);
        _user.Type = (UserType)request.Type;
        _user.IsActive = request.isActive is null ? _user.IsActive : request.isActive.Value;

        _userUpdateOnlyRepository.Update(_user);

        await _unitOfWork.CommitAsync();
    }

    private void Validate(RequestRegisterUserJson request)
    {
        var result = new RegisterUserValidator().Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors
            .Where(e => e.PropertyName != nameof(request.Password))
            .Select(e => e.ErrorMessage)
            .ToList();

            if (errorMessages.Any())
                throw new ErrorOnValidationException(errorMessages);
        }
    }
}
