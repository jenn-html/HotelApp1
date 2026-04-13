using HotelApp1.Data;
using HotelApp1.Models;
using HotelApp1.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BookingRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Booking booking)
        {

            _dbContext.Bookings.Add(booking);
            _dbContext.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var existingBooking = GetById(id);
            if (!(existingBooking == null && existingBooking.CheckIn <= DateTime.Today))
            {
                existingBooking.IsActive = false;
                _dbContext.SaveChanges();
            }
            else if (existingBooking.CheckIn <= DateTime.Today) 
            {
                Console.WriteLine("You can only delete a booking that haven't already started.");
            }
            else if (existingBooking == null){
                Console.WriteLine($"Can't find booking with id :{id}");
            }
        }

        public List<Booking> GetAllBookings()
        {
            return _dbContext.Bookings
                .Include(b => b.Guest)             // Hämtar gästens detaljer
                .Include(b => b.Room)              // Hämtar rumsdetaljer
                    .ThenInclude(r => r.RoomType)
                    // Går ett steg djupare och hämtar RoomType från Room
                .Where(b => b.IsActive)
                // Filtrerar på aktiva bokningar
                .ToList();
        }

        public Booking? GetById(int id)
        {
            return _dbContext.Bookings.FirstOrDefault(b => b.Id == id);
        }

        public void Update(Booking booking)
        {
            _dbContext.Bookings.Update(booking);
            _dbContext.SaveChanges();
        }
    }
}
