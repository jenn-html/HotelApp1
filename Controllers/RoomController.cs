using HotelApp1.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Controllers
{
    public class RoomController
    {
        private readonly RoomService _roomService;

        public RoomController(RoomService roomService)
        {
            _roomService = roomService;
        }
        public void ShowAllRooms()
        {
            var rooms = _roomService.GetAllRooms();
            foreach (var room in rooms)
            {

            }
        }
    }

}
