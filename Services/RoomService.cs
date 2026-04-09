using HotelApp1.Models;
using HotelApp1.Repositories;
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
        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public List<Room> GetAllRooms()
        {
            return _roomRepository.GetAllRooms();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
