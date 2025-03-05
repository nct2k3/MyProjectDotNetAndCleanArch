//Dto for data authen of presentation
namespace Contracts
{
    public record AuthenticationResponse(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string Token
        ,string Role


    );
}