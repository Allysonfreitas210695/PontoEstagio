using Moq;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories.EmailTemplate;

namespace CommonTestUltilities.Repositories;

public class EmailTemplateReadOnlyRepositoryMockBuilder
{
    private readonly Mock<IEmailTemplateReadOnlyRepository> _mock;

    public EmailTemplateReadOnlyRepositoryMockBuilder()
    {
        _mock = new Mock<IEmailTemplateReadOnlyRepository>();
    }

    public EmailTemplateReadOnlyRepositoryMockBuilder SetupGetTemplateByTitle(string title, EmailTemplates template)
    {
        _mock.Setup(x => x.GetEmailTemplatesByTitle(title))
            .ReturnsAsync(template);
        return this;
    }

    public IEmailTemplateReadOnlyRepository Build()
    {
        return _mock.Object;
    }

    public Mock<IEmailTemplateReadOnlyRepository> GetMock() => _mock;
}
