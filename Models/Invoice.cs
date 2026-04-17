using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text;

namespace HotelApp1.Models
{
    public class Invoice
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        public required List<string> Description { get; set; }
        [Required]
        public required decimal TotalAmount { get; set; }
        [Required]
        public DateTime Created {  get; set; } = DateTime.Today;
        [Required]
        public bool IsPayed { get; set; } = false;
        [Required]
        public bool IsActive { get; set; } = true;
        [Required]
        public required DateTime LastDayToPay { get; set; } = DateTime.Now.AddDays(10);   
    }
}
