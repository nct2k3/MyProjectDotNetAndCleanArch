namespace Contracts
{
    public record AuthenticationResponse(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string Phone,
        string Address,
        string Token
        ,string Role


    );
}