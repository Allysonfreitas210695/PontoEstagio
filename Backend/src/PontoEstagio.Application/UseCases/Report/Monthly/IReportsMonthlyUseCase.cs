using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.Reports.Monthly;
public interface IReportsMonthlyUseCase
{
    Task<List<ResponseReportMonthlyJson>> Executar(string periodo);
}
