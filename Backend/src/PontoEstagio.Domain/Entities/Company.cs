using PontoEstagio.Domain.Common;
using PontoEstagio.Domain.ValueObjects;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;
namespace PontoEstagio.Domain.Entities;

public class Company : Entity
{
    public string Name { get; private set; } = string.Empty;
    public string CNPJ { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;
    public Email Email { get; private set; } = default!;
    public bool IsActive { get; private set; }
    
    public virtual ICollection<Project> Projects { get; private set; } = new List<Project>();

    protected Company() { } 

    public Company(
        Guid? id,
        string name, 
        string cnpj,  
        string phone, 
        Email email
    )
    {
        Id = id ?? Guid.NewGuid();

        if (string.IsNullOrWhiteSpace(name))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.NameCannotBeEmpty });
        Name = name;

        if (string.IsNullOrWhiteSpace(cnpj))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.CnpjCannotBeEmpty });

        if (!ValidateCNPJ(cnpj))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.InvalidCnpjFormat });

        CNPJ = cnpj; 
        Phone = phone;
        Email = email;
        IsActive = true;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.NameCannotBeEmpty });

        Name = name;
        UpdateTimestamp();
    }

    public void UpdateCNPJ(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.CnpjCannotBeEmpty });

        if (!ValidateCNPJ(cnpj))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.InvalidCnpjFormat });

        CNPJ = cnpj;
        UpdateTimestamp();
    }

    public void UpdatePhone(string phone)
    {
        Phone = phone;
        UpdateTimestamp();
    }

    public void UpdateEmail(string email)
    {
        Email = Email.Criar(email);
        UpdateTimestamp();
    }

    public void Activate()
    {
        IsActive = true;
        UpdateTimestamp();
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdateTimestamp();
    }

    

    private bool ValidateCNPJ(string cnpj)
    {
        cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
        
        if (cnpj.Length != 14)
            return false;
            
        return true;
    }
}