using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Repositories.University;
using PontoEstagio.Exceptions.Exceptions;

namespace PontoEstagio.Application.UseCases.University.GetUniversityById;

public class GetUniversityByIdUseCase : IGetUniversityByIdUseCase
{
    private readonly IUniversityReadOnlyRepository _universityReadOnlyRepository;

    public GetUniversityByIdUseCase(IUniversityReadOnlyRepository universityReadOnlyRepository)
    {
        _universityReadOnlyRepository = universityReadOnlyRepository;
    }

    public async Task<ResponseUniversityJson> Execute(Guid id)
    {
        var university = await _universityReadOnlyRepository.GetUniversityByIdAsync(id);
        if(university is null)
            throw new NotFoundException("");

        return new ResponseUniversityJson() {
            Id = university.Id,
            Name = university.Name,
            Phone = university.Phone,
            Acronym = university.Acronym,
            Address = new ResponseAddressJson() {
                City = university.Address.City,
                Complement = university.Address.Complement,
                District = university.Address.District,
                Number = university.Address.Number,
                State = university.Address.State,
                Street = university.Address.Street
            },
            CNPJ = university.CNPJ,
            Email = university.Email.Endereco,
            IsActive = university.IsActive,
        };
    }
}