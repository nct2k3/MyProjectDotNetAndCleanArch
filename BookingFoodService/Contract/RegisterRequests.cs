namespace Contract
{
    public record RegisterRequests(
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber,
        string Address,
        string Password,
        string Token,
        string Role

    );
}