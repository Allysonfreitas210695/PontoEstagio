﻿using PontoEstagio.Communication.Request;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.User;
using PontoEstagio.Domain.Security.Cryptography;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

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
            throw new NotFoundException(ErrorMessages.UserNotFound);

        var registrationExists = await _userUpdateOnlyRepository.ExistOtherUserWithSameRegistrationAsync(_user.Id, request.Registration);
        if (registrationExists)
            throw new BusinessRuleException(ErrorMessages.RegistrationAlreadyInUse);

        _user.UpdateName(request.Name); 
        _user.UpdateType((UserType)request.Type);
        _user.UpdateEmail(request.Email);
        _user.UpdateUniversityId(request.UniversityId);
        _user.UpdatePhone(request.Phone);

        if(string.IsNullOrWhiteSpace(request.Registration))
            _user.UpdateRegistration(request.Registration);

        if(request.CourseId is not null)
             _user.UpdateCourseId(request.CourseId.Value);

        if(request.Type == Communication.Enum.UserType.Advisor)
        {
            if(string.IsNullOrWhiteSpace(request.CPF))
                _user.UpdateCpf(request.CPF);

            if (string.IsNullOrWhiteSpace(request.Department))
                _user.UpdateCpf(request.Department);
        }

       
        if (request.isActive == true) _user.Activate();
        else if (request.isActive == false) _user.Inactivate();
            
        _userUpdateOnlyRepository.Update(_user);

        await _unitOfWork.CommitAsync();
    }

    private void Validate(RequestRegisterUserJson request)
    {
        var result = new RegisterUserValidator().Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            if (errorMessages.Any())
                throw new ErrorOnValidationException(errorMessages);
        }
    }
}
