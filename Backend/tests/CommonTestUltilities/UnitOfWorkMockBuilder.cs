using Moq;
using PontoEstagio.Domain.Repositories;
using System.Threading.Tasks;

namespace CommonTestUltilities.Repositories;

public class UnitOfWorkMockBuilder
{
    private readonly Mock<IUnitOfWork> _mock;

    public UnitOfWorkMockBuilder()
    {
        _mock = new Mock<IUnitOfWork>();
    }

    public UnitOfWorkMockBuilder SetupCommitAsync()
    {
        _mock.Setup(x => x.CommitAsync())
            .Returns(Task.CompletedTask);
        return this;
    }

    public IUnitOfWork Build()
    {
        return _mock.Object;
    }

    public Mock<IUnitOfWork> GetMock()
    {
        return _mock;
    }
}