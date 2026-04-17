using HotelApp1.Data;
using HotelApp1.Models;
using HotelApp1.Utilities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Repositories
{
    public class RoomRepository : ICRUDRoom
    {
        private readonly ApplicationDbContext _dbContext;

        public RoomRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public void Delete(Room room)
        {
            room.IsActive = false;
               _dbContext.SaveChanges();    
        }
        public List<Booking> GetRoomThatHasBookings(Room room)
        {
            return
                _dbContext.Bookings.Where(b => b.IsActive == true)
                .Where(b => b.Room.Id == room.Id)
                .Where(b => b.CheckOut >= DateTime.Now).ToList();
        }

        public List<Room> GetAllRooms()
        {
            return _dbContext.Rooms.Where(b => b.IsActive)
                .Include(r => r.RoomType)
                .OrderByDescending(b => b.RoomSize)
                .ToList();
        }

        public Room GetById(int id)
        {
            return _dbContext.Rooms.FirstOrDefault(b => b.Id == id);
        }

        public void Update(Room room, Room roomToUpdate)
        {
            roomToUpdate.PricePerNight = room.PricePerNight;
            roomToUpdate.RoomSize = room.RoomSize;
            roomToUpdate.PricePerSquarefeet = room.PricePerSquarefeet;
            roomToUpdate.RoomType = room.RoomType;
            roomToUpdate.ExtraBedsAvailable = room.ExtraBedsAvailable;
            roomToUpdate.PriceExtraBeds = room.PriceExtraBeds;
            roomToUpdate.MaxGuestsPerRoomWithoutExtrabeds = room.MaxGuestsPerRoomWithoutExtrabeds;
            roomToUpdate.ExtraBedsAdded = room.ExtraBedsAdded;
            roomToUpdate.IsActive = room.IsActive;
            _dbContext.SaveChanges();
        }

        public void AddRoom(Room room)
        {
            _dbContext.Rooms.Add(room);
            _dbContext.SaveChanges();
        }

        public RoomType GetRoomtypeById(int id)
        {
            return _dbContext.RoomTypes.FirstOrDefault(rt => rt.Id == id);      
        }
    }
}
