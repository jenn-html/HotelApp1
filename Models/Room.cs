using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelApp1.Models
{
    public class Room
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string RoomType { get; set; }

        [Required]
        public int RoomSize { get; set; }

        [Required]
        public int RoomNumber { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1,4)]
        public int MinMaxGuestsPerRoom { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;
        
    }
}
