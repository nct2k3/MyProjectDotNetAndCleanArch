
using Application.Common.Interfaces.Persistance;
using Presentation.Entities;

namespace Infrastructure.Persistance;

public class UserRepository:IUserReopsitory
{
    public static List<User> _users=new();
    public User? GetUserByEmail(string email)
    {
       return _users.FirstOrDefault(u => u.Email == email);
    }

    public void AddUser(User user)
    {
       _users.Add(user);
    }
}