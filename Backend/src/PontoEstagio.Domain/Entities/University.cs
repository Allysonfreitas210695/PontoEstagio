using PontoEstagio.Domain.Common;
using PontoEstagio.Domain.ValueObjects;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Domain.Entities;

public class University : Entity
{
    public string Name { get; private set; }
    public string Acronym { get; private set; }
    public string CNPJ { get; private set; } = string.Empty;
    public Email Email { get; private set; }
    public string Phone { get; private set; }
    public bool IsActive { get; private set; }
    public Address Address { get; private set; }
    public ICollection<User> Users { get; private set; } = new List<User>();
    public ICollection<Project> Projects { get; private set; } = new List<Project>();


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
            throw new ErrorOnValidationException(new List<string> { "" });

        if (string.IsNullOrWhiteSpace(acronym)) 
            throw new ErrorOnValidationException(new List<string> { "" });
            
        if (string.IsNullOrWhiteSpace(cnpj))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.CnpjCannotBeEmpty });

        if (!ValidateCNPJ(cnpj))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.InvalidCnpjFormat });

        if (string.IsNullOrWhiteSpace(phone)) 
            throw new ErrorOnValidationException(new List<string> { "" });

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
}