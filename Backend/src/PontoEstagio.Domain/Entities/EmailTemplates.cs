using PontoEstagio.Domain.Common;

namespace PontoEstagio.Domain.Entities;

public class EmailTemplates : Entity
{
    public string Title { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;  
    public string Body { get; set; } = string.Empty;
}