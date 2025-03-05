
using Application.Common.Interfaces.Persistance;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Presentation.Entities;

namespace Infrastructure.Persistance;

public class UserRepository:IUserReopsitory
{

    public ApplicationDbContext _DbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _DbContext = dbContext;
    }
    
    public async Task<User?> GetUserByEmail(string email)
    {
        return await _DbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }



    public async Task AddUser(User user)
    {
        await _DbContext.Users.AddAsync(user);
        await _DbContext.SaveChangesAsync();
    }

}