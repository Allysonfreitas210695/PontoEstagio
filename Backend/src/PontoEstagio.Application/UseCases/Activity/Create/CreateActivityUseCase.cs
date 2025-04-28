using PontoEstagio.Communication.Requests;
using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Enum;

namespace PontoEstagio.Application.UseCases.Activity.Create
{
    public class CreateActivityUseCase : ICreateActivityUseCase
    {
        public async Task<ResponseActivityJson> ExecuteAsync(CreateActivityRequest request)
        {
            return new ResponseActivityJson
            {
                Id = Guid.NewGuid(),
                Description = request.Description,
                RecordedAt = DateTime.UtcNow,
                Status = ActivityStatus.Pending.ToString()
            };
        }
    }
}
