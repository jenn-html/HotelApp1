using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelApp1.Models
{
    public class RoomType
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public required string Type { get; set; }
    }
}
