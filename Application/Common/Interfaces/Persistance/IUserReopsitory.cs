using Presentation.Entities;

namespace Application.Common.Interfaces.Persistance;

public interface IUserReopsitory
{
    User? GetUserByEmail(string email);
    
    void AddUser(User user);
}