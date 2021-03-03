﻿using System.Collections.Generic;

namespace FoodPal.Delivery.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public List<Delivery> Deliveries { get; set; }
    }
}
