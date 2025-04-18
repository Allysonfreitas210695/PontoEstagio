using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Repositories;

namespace PontoEstagio.Application.UseCases.Users.GetAllUsers;
public class GetAllUsersUseCase : IGetAllUsersUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
  
    public GetAllUsersUseCase(IUserReadOnlyRepository userReadOnlyRepository)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
    }

    public async Task<List<ResponseShortUserJson>> Execute()
    {
        var result = await _userReadOnlyRepository.GetAllUsersAsync();
        return result.Select(z => new ResponseShortUserJson() {
            Id = z.Id,
            Name = z.Name,
            IsActive = z.IsActive,
            Type = z.Type.ToString()
        }).ToList();
    }
}
