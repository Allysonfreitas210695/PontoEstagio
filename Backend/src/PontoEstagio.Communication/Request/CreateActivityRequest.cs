using Microsoft.AspNetCore.Http;

namespace PontoEstagio.Communication.Requests
{
    public class CreateActivityRequest
    {
        public Guid AttendanceId { get; set; }
        public string Description { get; set; } = string.Empty;
        public IFormFile? ProofFile { get; set; }
    }
}
