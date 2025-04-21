
using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories.Report;
using PontoEstagio.Exceptions.Exceptions;

namespace PontoEstagio.Application.UseCases.Reports.Monthly;

public class ReportsMonthlyUseCase : IReportsMonthlyUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IReportReadOnlyRepository _reportReadOnlyRepository;
    public ReportsMonthlyUseCase(
        ILoggedUser loggedUser,
        IReportReadOnlyRepository reportReadOnlyRepository
    )
    {
        _loggedUser = loggedUser;
        _reportReadOnlyRepository = reportReadOnlyRepository;
    }

    public async Task<List<ResponseReportMonthlyJson>> Executar(string periodo)
    {
        if (!DateTime.TryParseExact($"{periodo}-01", "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out var startDate))
            throw new ArgumentException("Período inválido. Use o formato 'yyyy-MM'.");

        var endDate = startDate.AddMonths(1).AddDays(-1);

        var _user = await _loggedUser.Get();
        if (_user is null)
            throw new NotFoundException("User not found.");

        List<Domain.Entities.Attendance> attendances = new();

        if(_user.Type == Domain.Enum.UserType.Supervisor)
            attendances = await _reportReadOnlyRepository.GetBySupervisorAndPeriod(_user.Id, startDate, endDate);
        if(_user.Type == Domain.Enum.UserType.Intern)
            attendances = await _reportReadOnlyRepository.GetByInternAndPeriod(_user.Id, startDate, endDate);

        return attendances
                        .GroupBy(a => new { a.Date.Date, a.Status })
                        .Select(g => new ResponseReportMonthlyJson
                        {
                           Reference = g.Key.Date.ToString("yyyy-MM-dd"),
                           Status = g.Key.Status.ToString(),
                           TotalHours = Math.Round(g.Sum(a => (a.CheckOut - a.CheckIn).TotalHours), 2)
                        })
                        .OrderBy(r => r.Reference)
                        .ThenBy(r => r.Status)
                        .ToList(); 
    }
}
