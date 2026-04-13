using HotelApp1.Data;
using HotelApp1.Models;
using HotelApp1.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Repositories
{
    public class GuestRepository : IGuestRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public GuestRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public void Add(Guest guest)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            var existingGuest = GetById(id);
            List<Booking> hasActiveBooking = GetActiveBookingsByGuestId(id);
            if (hasActiveBooking != null)
            {
                Console.WriteLine("You can only delete a guest that don't have an active booking.");
            }
            else if (!(existingGuest == null && hasActiveBooking != null))
            {
                existingGuest.IsActive = false;
                _dbContext.SaveChanges();
            }
            else if (existingGuest == null)
            {
                Console.WriteLine($"Can't find guest with id :{id}");
            }
        }

        public List<Guest> GetAll()
        {
            return _dbContext.Guests          
               .Include(g => g.Bookings)              
                   .ThenInclude(r => r.Room)
               .Where(g => g.IsActive)
               .ToList();
        }

        public Guest GetById(int id)
        {
            return _dbContext.Guests.FirstOrDefault(g => g.Id == id);
        }
        public List<Booking> GetActiveBookingsByGuestId(int id)
        {
           return _dbContext.Bookings
                .Include(b => b.Guest)
                .Where(b => b.CheckIn > DateTime.Today || b.CheckOut >= DateTime.Today)
                .Where(b => b.Guest.Id == id).ToList();        
        }

        public void Update(Guest guest)
        {
            _dbContext.Guests.Update(guest);
            _dbContext.SaveChanges();
        }
    }
}
