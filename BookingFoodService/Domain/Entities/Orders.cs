using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; } 

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public string Status { get; set; } = "pending";

        public decimal TotalAmount { get; set; }
        
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;
    }
}