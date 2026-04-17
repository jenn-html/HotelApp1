using HotelApp1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Utilities.Interfaces
{
    public interface ICRUDRoom
    {
        List<Room> GetAllRooms();
        public Room GetById(int id);
        void AddRoom(Room room);
        void Update(Room room, Room room2);
        void Delete(Room room);
        public RoomType GetRoomtypeById(int id);
    }
}
