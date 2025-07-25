using PontoEstagio.Domain.Common;
using PontoEstagio.Domain.ValueObjects;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Domain.Entities;

public class University : Entity
{
    public string Name { get; private set; } = string.Empty;
    public string Acronym { get; private set; } = string.Empty;
    public string CNPJ { get; private set; } = string.Empty;
    public Email Email { get; private set; } = default!;
    public string Phone { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
    public Address Address { get; private set; } = default!;
    public ICollection<User> Users { get; private set; } = new List<User>();
    public ICollection<Project> Projects { get; private set; } = new List<Project>();
    public ICollection<Course> Courses { get; private set; } = new List<Course>();
    
    public University()
    {
        
    }

    public University(
        Guid? id, 
        string name,
        string acronym,
        string cnpj, 
        Email email,
        string phone,
        bool isActive,
        Address address
    ) {
        Id = id ?? Guid.NewGuid();

        if (string.IsNullOrWhiteSpace(name))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.University_NameRequired });

        if (string.IsNullOrWhiteSpace(acronym))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.University_AcronymRequired });

        if (string.IsNullOrWhiteSpace(cnpj))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.CnpjCannotBeEmpty });

        if (!ValidateCNPJ(cnpj))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.InvalidCnpjFormat });

        if (string.IsNullOrWhiteSpace(phone))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.University_PhoneRequired });

        Name = name;
        Acronym = acronym;
        CNPJ = cnpj;
        Phone = phone;
        Email = email;
        IsActive = isActive;
        Address = address;
    }

    private bool ValidateCNPJ(string cnpj)
    {
        cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
        
        if (cnpj.Length != 14)
            return false;
            
        return true;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.University_NameRequired });

        Name = name;
        UpdateTimestamp();
    }

    public void UpdateAcronym(string acronym)
    {
         if (string.IsNullOrWhiteSpace(acronym))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.University_AcronymRequired });
        
        Acronym = acronym;
        UpdateTimestamp();
    }

    public void UpdateCNPJ(string cnpj)
    {
        if(ValidateCNPJ(cnpj) == false) return;

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

    public void UpdateAddress(Address address){
        Address = address;
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
}