﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop.Shared
{
    public class Address
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Line1 { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Line2 { get; set; } = string.Empty;

        [Required(ErrorMessage = "How do you expect to receive the pizza if we don't even know what city you're in?"), MaxLength(50)]
        public string City { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string Region { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string PostalCode { get; set; } = string.Empty;
    }
}
