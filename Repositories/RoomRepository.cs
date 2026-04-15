using HotelApp1.Data;
using HotelApp1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Repositories
{
    public class RoomRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RoomRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public void Add(Room room)
        {
            _dbContext.Rooms.Add(room);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var existingRoom = GetById(id);
            
            if (!( existingRoom == null)) 
            {
                var hasBookings = _dbContext.Bookings.Where(b => b.IsActive == true)
                .Where(b => b.Room.Id == existingRoom.Id)
                .Where(b => b.CheckOut >= DateTime.Now).ToList();

                if (!(hasBookings == null))
                {
                    Console.WriteLine("Can't delete a room that have bookings!");
                    Console.WriteLine("Current bookings:");
                    foreach (var booking in hasBookings)
                    {
                        Console.WriteLine(booking);
                        Console.WriteLine("-------------------");
                    }
                }
                else
                {
                    existingRoom.IsActive = false;
                    Console.WriteLine($"Room {existingRoom.RoomNumber} deleted");
                    _dbContext.SaveChanges();
                }
            }
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

        public void Update(Room room)
        {
            _dbContext.Rooms.Update(room);
            _dbContext.SaveChanges();
        }
    }
}
