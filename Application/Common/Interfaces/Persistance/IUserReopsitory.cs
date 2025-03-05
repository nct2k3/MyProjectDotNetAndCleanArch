using Presentation.Entities;

namespace Application.Common.Interfaces.Persistance;

public interface IUserReopsitory
{
    Task<User?> GetUserByEmail(string email);
    
    Task AddUser(User user);
}