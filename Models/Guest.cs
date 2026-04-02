using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HotelApp1.Models
{
    public class Guest
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public required string FirstName { get; set; }


        [MaxLength(50)]
        public required string LastName { get; set; }


        [MaxLength(100)]
        public required string Street { get; set; }


        [MaxLength (10)]
        public required string PostalCode { get; set; }


        [MaxLength(50)]
        public required string City { get; set; }

        [MaxLength(50)]
        public required string Country { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        
        public List <Booking> Bookings { get; set; } = new List<Booking>();
        
        public List <Invoice> Invoices { get; set; } = new List<Invoice>();

    }
}
