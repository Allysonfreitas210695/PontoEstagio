using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using PontoEstagio.Application.UseCases.Activity.Create;
using PontoEstagio.Communication.Requests;
using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories.Activity;
using PontoEstagio.Domain.Repositories.Attendance;
using PontoEstagio.Infrastructure.Services.LoggedUser;
using Xunit;

namespace PontoEstagio.Tests.UseCases.Activity.Create
{
    public class CreateActivityUseCaseTest
    {
        private readonly Mock<IActivityWriteOnlyRepository> _activityWriteOnlyRepositoryMock;
        private readonly Mock<IAttendanceReadOnlyRepository> _attendanceReadOnlyRepositoryMock;
        private readonly Mock<ILoggedUser> _loggedUserMock;
        private readonly Mock<IWebHostEnvironment> _webHostEnvironmentMock;
        private readonly CreateActivityUseCase _useCase;

        public CreateActivityUseCaseTest()
        {
            _activityWriteOnlyRepositoryMock = new Mock<IActivityWriteOnlyRepository>();
            _attendanceReadOnlyRepositoryMock = new Mock<IAttendanceReadOnlyRepository>();
            _loggedUserMock = new Mock<ILoggedUser>();
            _webHostEnvironmentMock = new Mock<IWebHostEnvironment>();

            _webHostEnvironmentMock.Setup(w => w.WebRootPath).Returns("wwwroot");

            _useCase = new CreateActivityUseCase(
                _activityWriteOnlyRepositoryMock.Object,
                _attendanceReadOnlyRepositoryMock.Object,
                _loggedUserMock.Object,
                _webHostEnvironmentMock.Object
            );
        }

        [Fact]
        public async Task Deve_Criar_Atividade_Com_Sucesso()
        {
            // Arrange
            var attendanceId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            _attendanceReadOnlyRepositoryMock.Setup(r => r.GetByIdAsync(attendanceId))
                .ReturnsAsync(new Attendance
                {
                    Id = attendanceId,
                    ProjectId = Guid.NewGuid()
                });

            _loggedUserMock.Setup(u => u.UserId).Returns(userId);

            var request = new CreateActivityRequest
            {
                AttendanceId = attendanceId,
                Description = "Testando atividade",
                ProofFile = null // sem arquivo para simplificar primeiro teste
            };

            // Act
            var response = await _useCase.ExecuteAsync(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(request.Description, response.Description);
            Assert.Equal("Pending", response.Status); // status inicial
            _activityWriteOnlyRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Activity>()), Times.Once);
        }
    }
}
// O que esse teste faz:

// Etapa	Ação
// Arrange	Cria mocks dos repositórios e ambiente
// Act	Executa o ExecuteAsync passando uma requisição
// Assert	Verifica se a resposta não é nula, se o Description e Status estão corretos, e se AddAsync foi chamado