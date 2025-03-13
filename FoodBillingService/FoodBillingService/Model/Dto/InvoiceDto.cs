namespace FoodBillingService.Model.Dto;

public class Order
{
    public Guid OrderId { get; set; }
    public User User { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderDetail> Details { get; set; }
}

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}

public class OrderDetail
{
    public Guid Id { get; set; }
    public Guid FoodId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}