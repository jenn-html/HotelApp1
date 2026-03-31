using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace HotelApp1.Models
{
    internal class Booking
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [Timestamp]
        public DateTime CheckIn { get; set; }

        [Required]
        [Timestamp]
        public DateTime CheckOut {  get; set; }

        [Required]
        public int NumberOfGuests {  get; set; }

        [Required]
        public Customer CustomerId { get; set; }

        [Required]
        public Room RoomId { get; set; }

        [Required]
        public Invoice InvoiceId { get; set; }

        [Required]
        public bool IsPayed { get; set; }
        
        [Required]
        public bool IsActive { get; set; }
    }
}
