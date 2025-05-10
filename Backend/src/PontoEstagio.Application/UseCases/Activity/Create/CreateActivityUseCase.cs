using PontoEstagio.Communication.Requests;
using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Domain.Repositories.Activity;
using PontoEstagio.Domain.Repositories.Attendance;
using PontoEstagio.Infrastructure.Services.LoggedUser;

namespace PontoEstagio.Application.UseCases.Activity.Create
{
    public class CreateActivityUseCase : ICreateActivityUseCase
    {
        private readonly IActivityWriteOnlyRepository _activityWriteOnlyRepository;
        private readonly IAttendanceReadOnlyRepository _attendanceReadOnlyRepository;
        private readonly ILoggedUser _loggedUser;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateActivityUseCase(
            IActivityWriteOnlyRepository activityWriteOnlyRepository,
            IAttendanceReadOnlyRepository attendanceReadOnlyRepository,
            ILoggedUser loggedUser,
            IWebHostEnvironment webHostEnvironment
        )
        {
            _activityWriteOnlyRepository = activityWriteOnlyRepository;
            _attendanceReadOnlyRepository = attendanceReadOnlyRepository;
            _loggedUser = loggedUser;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ResponseActivityJson> ExecuteAsync(CreateActivityRequest request)
        {
            // 1. Verificar se a frequência (attendance) existe
            var attendance = await _attendanceReadOnlyRepository.GetByIdAsync(request.AttendanceId);
            if (attendance == null)
            {
                throw new Exception("Frequência não encontrada.");
            }

            // 2. Pegar o usuário logado
            var userId = _loggedUser.UserId;

            // 3. Preparar o caminho para o upload do arquivo, se houver
            string? proofFilePath = null;
            if (request.ProofFile != null && request.ProofFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath ?? "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{request.ProofFile.FileName}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.ProofFile.CopyToAsync(fileStream);
                }

                proofFilePath = $"uploads/{uniqueFileName}";
            }

            // 4. Criar o objeto de Activity
            var activity = new Domain.Entities.Activity(
                id: null,
                attendanceId: attendance.Id,
                userId: userId,
                projectId: attendance.ProjectId,
                description: request.Description,
                recordedAt: DateTime.UtcNow,
                proofFilePath: proofFilePath
            );

            // 5. Salvar no banco
            await _activityWriteOnlyRepository.AddAsync(activity);

            // 6. Retornar o objeto de resposta
            return new ResponseActivityJson
            {
                Id = activity.Id,
                Description = activity.Description,
                RecordedAt = activity.RecordedAt,
                Status = activity.Status.ToString()
            };
        }
    }
}
