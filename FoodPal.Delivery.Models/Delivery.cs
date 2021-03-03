using FoodPal.Delivery.Enums;
using System;

namespace FoodPal.Delivery.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public DeliveryStatus Status { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
        public string Info { get; set; }

    }
}
