using HotelApp1.Models;
using HotelApp1.Repositories;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Services
{
    public class RoomService 
    {
        private readonly RoomRepository _roomRepository;
        public RoomService(RoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        public void AddNewRoom(Room room)
        {
            _roomRepository.AddRoom(room);
        }

        public void DeleteRoom(Room room)
        {
            _roomRepository.Delete(room);
        }

        public List<Room> GetAllRooms()
        {
            return _roomRepository.GetAllRooms();
        }

        public void Update(Room room, Room roomToUpdate)
        {
            _roomRepository.Update(room, roomToUpdate);
        }

        public Room GetById(int roomId)
        {
            return _roomRepository.GetById(roomId);
        }

        public RoomType GetRoomtypeById(int id)
        {
            return _roomRepository.GetRoomtypeById(id);
        }

        public List<Booking> GetRoomThatHasBookings(Room room)
        {
            return _roomRepository.GetRoomThatHasBookings(room);
        }
    }
}
