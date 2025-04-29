using Microsoft.EntityFrameworkCore;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories.EmailTemplate;
using PontoEstagio.Infrastructure.Context;

namespace PontoEstagio.Infrastructure.DataAccess.Repositories;
public class EmailTemplateRepository : IEmailTemplateReadOnlyRepository
{
    private readonly PontoEstagioDbContext _dbContext;

    public EmailTemplateRepository(PontoEstagioDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<EmailTemplates?> GetEmailTemplatesByTitle(string title)
    {
        return await _dbContext.EmailTemplates.AsNoTracking().Where(x => x.Title == title).FirstOrDefaultAsync();
    }
}
