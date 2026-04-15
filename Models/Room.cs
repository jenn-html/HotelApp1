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
        
        public int RoomNumber { get; set; }

        [Required]
        [Range(1,4)]
        public int MaxGuestsPerRoomWithoutExtrabeds { get; set; }

        [Range(0, 2)]
        public int ExtraBedsAvailable { get; set; } = 0;

        public required decimal PricePerNight { get; set; } 

        public decimal PriceExtraBeds { get; set; } = 200;

        [Required]
        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

    }
}
