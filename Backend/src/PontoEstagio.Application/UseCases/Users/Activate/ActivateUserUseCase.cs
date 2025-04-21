
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.User; 
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Users.Delete;

public class ActivateUserUseCase : IActivateUserUseCase
{
    private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    public ActivateUserUseCase(
        IUserUpdateOnlyRepository userUpdateOnlyRepository,
        IUnitOfWork unitOfWork
    )
    {
        _userUpdateOnlyRepository = userUpdateOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid id)
    {
        var _user = await _userUpdateOnlyRepository.GetUserByIdAsync(id);
        if (_user is null)
            throw new NotFoundException(ErrorMessages.UserNotFound);

        _user!.Activate();

        _userUpdateOnlyRepository.Update(_user);

        await _unitOfWork.CommitAsync();
    }
}
