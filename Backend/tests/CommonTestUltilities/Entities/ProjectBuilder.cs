using Bogus;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Enum;

namespace CommonTestUtilities.Entities
{
    public class ProjectBuilder
    {
        public static Project Build(
            Guid? id = null,
            Guid? companyId = null,
            string? name = null,
            string? description = null,
            long? totalHours = null,
            ProjectStatus? status = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            Guid? createdBy = null)
        {
            var faker = new Faker();

            return new Project(
                id: id ?? Guid.NewGuid(),
                companyId: companyId ?? Guid.NewGuid(),
                name: name ?? faker.Commerce.ProductName(),
                description: description ?? faker.Lorem.Sentence(),
                totalHours: totalHours ?? faker.Random.Long(10, 1000),
                status: status ?? faker.PickRandom<ProjectStatus>(),
                startDate: startDate ?? faker.Date.Past(),
                endDate: endDate ?? faker.Date.Future(),
                createdBy: createdBy ?? Guid.NewGuid()
            );
        }
    }
}
