﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using Zomato.Entity.Enum;

namespace Zomato.Model
{
    public class Payment
    {
        public long id { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public PaymentMethod paymentMethod { get; set; }
        public Order order { get; set; }
        public Double amount { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public PaymentStatus paymentStatus { get; set; }
        public DateTime paymentTime { get; set; }
    }
}
