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
        public string Facilities { get; set; }

        [Required]
        [Range(1,6)]
        public int MinMaxGuestsPerRoom { get; set; }

        [Range(0, 2)]
        public int ExtraBeds { get; set; } = 0;

        [Range (0,2)]
        public int Crib { get; set; } = 0;

        [Required]
        public bool IsActive { get; set; } = true;
        
    }
}
