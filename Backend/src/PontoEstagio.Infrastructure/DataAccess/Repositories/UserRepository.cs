using Microsoft.EntityFrameworkCore;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.User;
using PontoEstagio.Infrastructure.Context;

namespace PontoEstagio.Infrastructure.Repositories;

public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository
{
    private readonly PontoEstagioDbContext _dbContext;

    public UserRepository(PontoEstagioDbContext dbContext) => _dbContext = dbContext;

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task<bool> ExistActiveUserWithEmailAsync(string email)
    {
        return await _dbContext.Users.AnyAsync(user => user.Email.Endereco.Equals(email));
    }

    public async Task<bool> ExistActiveUserWithRegistrationAsync(string registration)
    {
        return await _dbContext.Users.AnyAsync(u => u.Registration == registration && u.IsActive);
    }

    public async Task<bool> ExistOtherUserWithSameRegistrationAsync(Guid userId, string registration)
    {
        return await _dbContext.Users
            .AnyAsync(u => u.Registration == registration && u.Id != userId && u.IsActive);
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _dbContext.Users.AsNoTracking().AsNoTracking().ToListAsync();
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    { 
       return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Endereco.Equals(email));
    }

    async Task<User?> IUserReadOnlyRepository.GetUserByIdAsync(Guid id)
    {
        return await _dbContext.Users
                                .Include(x => x.Activities)
                                .Include(x => x.Attendances)
                                .Include(x => x.University)
                                .AsNoTracking()
                                .FirstOrDefaultAsync(user => user.Id == id);
    }

    async Task<User?> IUserUpdateOnlyRepository.GetUserByIdAsync(Guid id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
    }

    public void Update(User user)
    {
        _dbContext.Users.Update(user);
    }
}