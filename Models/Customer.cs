using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HotelApp1.Models
{
    public class Customer
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required]
        [MaxLength (10)]
        public string PostalCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(50)]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;


        public List <Booking> Bookings { get; set; } = new List<Booking>();
        
        public List <Invoice> Invoices { get; set; } = new List<Invoice>();

    }
}
