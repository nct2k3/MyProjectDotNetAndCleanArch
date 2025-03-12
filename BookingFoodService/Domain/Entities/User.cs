using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class User
{
    [Key]public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int PhoneNumber { get; set; }
    public string Address { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; } = "User"; 
}