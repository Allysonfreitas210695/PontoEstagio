using Moq;
using PontoEstagio.Domain.Services.Email;

namespace CommonTestUltilities.Services;

public class EmailServiceMockBuilder
{
    private readonly Mock<IEmailService> _mock;

    public EmailServiceMockBuilder()
    {
        _mock = new Mock<IEmailService>();
    }

    public EmailServiceMockBuilder SetupSendEmail()
    {
        _mock.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.CompletedTask);
        return this;
    }

    public IEmailService Build() => _mock.Object;
    public Mock<IEmailService> GetMock() => _mock;
}
