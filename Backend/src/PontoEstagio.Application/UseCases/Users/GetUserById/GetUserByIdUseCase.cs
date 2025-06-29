﻿using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Repositories; 
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Users.GetUserById;
public class GetUserByIdUseCase : IGetUserByIdUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository; 

    public GetUserByIdUseCase(
        IUserReadOnlyRepository userReadOnlyRepository 
    )
    {
        _userReadOnlyRepository = userReadOnlyRepository;
     }

    public async Task<ResponseUserJson> Execute(Guid id)
    { 
        var result = await _userReadOnlyRepository.GetUserByIdAsync(id);
        if (result is null)
            throw new NotFoundException(ErrorMessages.UserNotFound);

        return new ResponseUserJson()
        {
            Id = result.Id,
            Name = result.Name,
            IsActive = result.IsActive,
            Type = result.Type.ToString(), 
            CreatedAt = result.CreatedAt 
        };
    }
}
