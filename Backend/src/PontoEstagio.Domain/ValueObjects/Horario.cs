namespace PontoEstagio.Domain.ValueObjects;

public class Horario
{
    public TimeSpan Valor { get; }

    private Horario(TimeSpan valor)
    {
        Valor = valor;
    }

    public static Horario Criar(TimeSpan valor)
    {
        if (valor < TimeSpan.Zero || valor > TimeSpan.FromHours(24))
            throw new ArgumentException("Horário fora do intervalo permitido");

        return new Horario(valor);
    }

    public override string ToString() => Valor.ToString(@"hh\:mm");
}
