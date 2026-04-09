using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HotelApp1.Models
{
    public class Room
    {
        [Key]
        [Required]
        public int Id { get; set; } 

        public  required RoomType RoomType { get; set; }

        [Required]
        [Range (10, 50)]
        public double RoomSize { get; set; }

        [Required]
        
        public int RoomNumber { get; set; }

        [Required]
        [Range(1,4)]
        public int MinMaxGuestsPerRoom { get; set; }

        [Range(0, 2)]
        public int ExtraBeds { get; set; } = 0;

        [Required]
        public bool IsActive { get; set; } = true; 

    }
}
