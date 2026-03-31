using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Models
{
    internal class Invoice
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
