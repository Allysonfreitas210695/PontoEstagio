using Microsoft.EntityFrameworkCore;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories.Cource;
using PontoEstagio.Infrastructure.Context;

namespace PontoEstagio.Infrastructure.DataAccess.Repositories;

public class CourceRepository : ICourceReadOnlyRepository, ICourceWriteOnlyRepository, ICourceUpdateOnlyRepository
{
    private readonly PontoEstagioDbContext _dbContext;
    public CourceRepository(PontoEstagioDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Course course)
    {
        await _dbContext.Courses.AddAsync(course);
    }

    public async Task<List<Course>> GetAllAsync()
    {
        return await _dbContext.Courses.Include(x => x.University).AsNoTracking().ToListAsync();
    }

    public void Update(Course course)
    {
        _dbContext.Courses.Update(course);
    }

    async Task<Course?> ICourceReadOnlyRepository.GetByIdAsync(Guid id)
    {
        return await _dbContext.Courses.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
    }

    async Task<Course?> ICourceUpdateOnlyRepository.GetByIdAsync(Guid id)
    {
        return await _dbContext.Courses.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
    }
}