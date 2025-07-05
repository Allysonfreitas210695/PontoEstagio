
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.LegalRepresentative;
using PontoEstagio.Exceptions.Exceptions;

namespace PontoEstagio.Application.UseCases.Company.DeleteLegalRepresentative;

public class DeleteLegalRepresentativeUseCase : IDeleteLegalRepresentativeUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly ILegalRepresentativeDeleteOnlyRepository _legalRepresentativeDeleteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLegalRepresentativeUseCase(
        ILoggedUser loggedUser,
        ILegalRepresentativeDeleteOnlyRepository legalRepresentativeDeleteOnlyRepository,
        IUnitOfWork unitOfWork
    )
    {
        _loggedUser = loggedUser;
        _legalRepresentativeDeleteOnlyRepository = legalRepresentativeDeleteOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid companyId, Guid representativeId)
    {
        var _user = await _loggedUser.Get();
         
        if (_user?.Type == Domain.Enum.UserType.Admin)
            throw new BusinessRuleException("Admins cannot delete legal representatives.");

        var legalRepresentative = await _legalRepresentativeDeleteOnlyRepository.GetByIdAsync(companyId, representativeId);
        if (legalRepresentative is null)
            throw new NotFoundException("Legal representative not found.");

        _legalRepresentativeDeleteOnlyRepository.Delete(legalRepresentative);
        await _unitOfWork.CommitAsync();
    }
}