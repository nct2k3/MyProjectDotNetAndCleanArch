using Domain.Entities;

namespace Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string Generator (User user);
    
}