namespace PontoEstagio.Domain.Services.Email;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}