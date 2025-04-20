using System.Text.RegularExpressions;

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
            throw new ArgumentException("Email não pode ser vazio");

        var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        if (!regex.IsMatch(endereco))
            throw new ArgumentException("Formato de email inválido");

        return new Email(endereco);
    }

    public override string ToString() => Endereco;
}