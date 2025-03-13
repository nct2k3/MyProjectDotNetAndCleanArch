using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FoodBillingService.Model.Entities
{
    public class Order
    {
        [BsonRepresentation(BsonType.String)] // Lưu Guid dưới dạng chuỗi
        public Guid OrderId { get; set; }
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderDetail> Details { get; set; }
    }

    public class User
    {
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class OrderDetail
    {
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid FoodId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}