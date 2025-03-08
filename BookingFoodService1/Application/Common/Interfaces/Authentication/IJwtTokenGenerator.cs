using Presentation.Entities;

namespace Application.Common.Interfaces;
// creat interface in application to call 
public interface IJwtTokenGenerator
{
    string  GenerateJwtToken(User user);
}