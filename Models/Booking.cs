using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;

namespace HotelApp1.Models
{
    public class Booking
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
       
        public DateTime CheckIn { get; set; }

        [Required]
        
        public DateTime CheckOut {  get; set; }

        [Required]
        public int NumberOfGuests {  get; set; }

        public required Guest Guest { get; set; }

        public required Room Room { get; set; }

        public  DateTime TimeOfBooking { get; set; } = DateTime.Now;
        
        [Required]
        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

    }
}
