using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Repositories.University;

namespace PontoEstagio.Application.UseCases.University.GetAllUniversities;

public class GetAllUniversitiesUseCase : IGetAllUniversitiesUseCase
{
    private readonly IUniversityReadOnlyRepository _universityReadOnlyRepository;

    public GetAllUniversitiesUseCase(IUniversityReadOnlyRepository universityReadOnlyRepository)
    {
        _universityReadOnlyRepository = universityReadOnlyRepository;
    }

    public async Task<List<ResponseUniversityJson>> Execute()
    {
        var universities = await _universityReadOnlyRepository.GetAllUniversitiesAsync();

        return universities.Select(z => new ResponseUniversityJson() {
            Id = z.Id,
            Name = z.Name,
            Phone = z.Phone,
            Acronym = z.Acronym,
            Address = new ResponseAddressJson()
            {
                City = z.Address.City,
                Complement = z.Address.Complement,
                District = z.Address.District,
                Number = z.Address.Number,
                State = z.Address.State,
                Street = z.Address.Street,
                ZipCode = z.Address.ZipCode
            },
            CNPJ = z.CNPJ,
            Email = z.Email.Endereco,
            IsActive = z.IsActive,
        }).ToList();
    }
}