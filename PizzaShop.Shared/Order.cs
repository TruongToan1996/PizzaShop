﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop.Shared
{
    public class Order
    {
        public int OrderId { get; set; }

        public string? UserId { get; set; }

        public DateTime CreatedTime { get; set; }
        public Address? DeliveryAddress { get; set; } = new Address();
        public LatLong? DeliveryLocation { get; set; } = new LatLong();  
        public List<Pizza>? Pizzas { get; set; } = new List<Pizza>();

        public decimal GetTotalPrice() => Pizzas.Sum(p => p.GetTotalPrice());

        public string GetFormattedTotalPrice() => GetTotalPrice().ToString("0.00");
    }
}
