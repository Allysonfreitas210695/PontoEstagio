using PontoEstagio.Domain.Common;
using PontoEstagio.Domain.ValueObjects;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Domain.Entities;
public class LegalRepresentative : Entity
{
    public string FullName { get; private set; } = string.Empty;
    public string CPF { get; private set; } = string.Empty;
    public string Position { get; private set; } = string.Empty;
    public Email Email { get; private set; } = default!;
    public Guid CompanyId { get; private set; }
    public virtual Company Company { get; private set; } = default!;

    protected LegalRepresentative() { }

    public LegalRepresentative(
        Guid? id,
        string fullName,
        string cpf,
        string position,
        Email email,
        Guid companyId
    )
    {
        Id = id ?? Guid.NewGuid();

        if (string.IsNullOrWhiteSpace(fullName))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.RepresentativeNameRequired });

        if (string.IsNullOrWhiteSpace(cpf))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.RepresentativeCpfRequired });

        if (string.IsNullOrWhiteSpace(position))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.RepresentativePositionRequired });

        FullName = fullName;
        CPF = cpf;
        Position = position;
        Email = email;
        CompanyId = companyId;
    }

    public void UpdateFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.RepresentativeNameRequired });

        FullName = fullName;
        UpdateTimestamp();
    }

    public void UpdateCPF(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.RepresentativeCpfRequired });

        CPF = cpf;
        UpdateTimestamp();
    }

    public void UpdatePosition(string position)
    {
        if (string.IsNullOrWhiteSpace(position))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.RepresentativePositionRequired });

        Position = position;
        UpdateTimestamp();
    }

    public void UpdateEmail(string email)
    {
        Email = Email.Criar(email);
        UpdateTimestamp();
    }

    public void UpdateCompanyId(Guid companyId)
    {
        if (companyId == Guid.Empty)
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.InvalidCompanyId });

        CompanyId = companyId;
        UpdateTimestamp();
    }
}
