using Domain.Entities;

namespace Application.Application.Common;

public record AuthenticationResult
( User User
,string Token
    );