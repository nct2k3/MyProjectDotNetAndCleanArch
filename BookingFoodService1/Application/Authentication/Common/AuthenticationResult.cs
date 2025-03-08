using Presentation.Entities;

namespace Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token
    
    
);