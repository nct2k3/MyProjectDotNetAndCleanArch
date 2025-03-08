namespace Contracts
{
    public record RegisterRequests(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string Token,
        string Role
        
    );
}