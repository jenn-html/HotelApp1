using HotelApp1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Repositories
{
    public interface IRoomRepository
    {
        List<Room> GetAllRooms();
        Booking? GetByDate(DateTime date);
        void Add(Room room);
        void Update(Room room);
        void Delete(int id);
    }
}
