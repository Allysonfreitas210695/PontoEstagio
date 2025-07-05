using System.Text.RegularExpressions;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Domain.ValueObjects;

public class Email
{
    public string Endereco { get; private set; } 

    public Email() {
        Endereco = string.Empty;
    }

    private Email(string endereco)
    {
        Endereco = endereco;
    }

    public static Email Criar(string endereco)
    {
        if (string.IsNullOrWhiteSpace(endereco))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.EmailCannotBeEmpty});

        var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        if (!regex.IsMatch(endereco))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.InvalidEmailFormat});


        return new Email(endereco);
    }

    public override string ToString() => Endereco;
}