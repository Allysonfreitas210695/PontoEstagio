using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using PontoEstagio.Domain.Services.Configuration;
using PontoEstagio.Domain.Services.Email;

namespace PontoEstagio.Infrastructure.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MailMessage
            {
                From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            message.To.Add(to);

            using (var smtp = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port))
            { 
                smtp.UseDefaultCredentials = false; 
                smtp.Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.Password);
                smtp.EnableSsl = true; 
                await smtp.SendMailAsync(message); 
            }
        }
    }
}
