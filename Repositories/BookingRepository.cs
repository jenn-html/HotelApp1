using HotelApp1.Data;
using HotelApp1.Models;
using HotelApp1.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BookingRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public void Add(Booking booking)
        {
            _dbContext.Bookings.Add(booking);
        }

        public void Delete(int id)
        {
            var existingBooking = GetById(id);
            if (existingBooking != null)
            {
                existingBooking.IsActive = false;
                existingBooking.Invoice.IsActive = false;
            }
        }

        public List<Booking> GetAllBookings()
        {
            return _dbContext.Bookings.Where(b => b.IsActive).ToList();
        }

        public Booking? GetById(int id)
        {
            return _dbContext.Bookings.FirstOrDefault(b => b.Id == id);
        }

        public void Update(Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}
