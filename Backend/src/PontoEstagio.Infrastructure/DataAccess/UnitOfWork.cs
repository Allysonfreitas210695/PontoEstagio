using PontoEstagio.Domain.Repositories;
using PontoEstagio.Infrastructure.Context;

internal class UnitOfWork : IUnitOfWork
{
    private readonly PontoEstagioDbContext _dbContext;

    public UnitOfWork(PontoEstagioDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CommitAsync() => await _dbContext.SaveChangesAsync();
}