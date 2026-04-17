using HotelApp1.Data;
using HotelApp1.Models;
using HotelApp1.Utilities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelApp1.Repositories
{
    public class GuestRepository : ICRUDGuest
    {
        private readonly ApplicationDbContext _dbContext;
        public GuestRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Guest guest)
        {
            _dbContext.Add(guest);
            _dbContext.SaveChanges();
        }

        public void DeleteGuest(Guest existingGuest)
        {
            
            existingGuest.IsActive = false;
            _dbContext.SaveChanges();
        }

        public List<Guest> GetAll()
        {
            return _dbContext.Guests
               .Include(g => g.Bookings)
                   .ThenInclude(r => r.Room)
               .ToList();
        }

        public Guest GetById(int id)
        {
            return _dbContext.Guests
                .Where(g => g.IsActive == true).FirstOrDefault(g => g.Id == id);
        }
        public List<Booking> GetActiveBookingsByGuestId(int id)
        {
            return _dbContext.Bookings
           .Include(b => b.Guest)
           .Include(b => b.Room)
           .Where(b =>
               b.Guest.Id == id
               && b.IsActive == true
               && (b.CheckOut.Date >= DateTime.Today)) 
           .ToList();
        }

        public void Update(Guest guest, Guest updatedGuest)
        {
            guest.FirstName = updatedGuest.FirstName;
            guest.LastName = updatedGuest.LastName;
            guest.Phone = updatedGuest.Phone;
            guest.Email = updatedGuest.Email;
            guest.Street = updatedGuest.Street;
            guest.PostalCode = updatedGuest.PostalCode;
            guest.City = updatedGuest.City;
            guest.Country = updatedGuest.Country;
            guest.IsActive = updatedGuest.IsActive;
            _dbContext.SaveChanges();
        }

        public List<Guest> GetAllGuestsToday()
        {
            return _dbContext.Guests
                .Include(g => g.Bookings)
                    .ThenInclude(b => b.Room)
                .Where(g => g.Bookings.Any(b => b.IsActive
                                               && b.CheckIn.Date <= DateTime.Today
                                               && b.CheckOut.Date > DateTime.Today))
                .OrderBy(b => b.FirstName)
                .ToList();
        }

        public List<Guest> GetAllDeletedGuests()
        {
            return _dbContext.Guests
            .Include(g => g.Bookings)
                .ThenInclude(r => r.Room)
            .Where(g => g.IsActive == false)
            .OrderBy(b => b.FirstName)
            .ToList();
        }

        public List<Guest> GetAllActiveGuests()
        {
            return _dbContext.Guests
            .Include(g => g.Bookings)
                .ThenInclude(r => r.Room)
            .Where(g => g.IsActive == true)
            .OrderBy(b => b.FirstName)
            .ToList();
        }

        public void AddDeletedGuest(Guest guestToAdd)
        {
            guestToAdd.IsActive = true;
            _dbContext.SaveChanges();
        }
    }
}
