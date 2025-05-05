namespace PontoEstagio.Domain.Repositories.EmailTemplate;

public interface IEmailTemplateReadOnlyRepository
{
    Task<Entities.EmailTemplates?> GetEmailTemplatesByTitle(string title);
}
