using HotelApp1.Data;
using HotelApp1.Models;
using HotelApp1.Services;
using HotelApp1.Utilities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelApp1.Repositories
{
    public class BookingRepository : ICRUDBooking
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

        public void DeleteBookingById(int id)
        {
            var existingBooking = GetBookingById(id);
            if (existingBooking == null)
            {
                Console.Clear();
                Console.WriteLine($"Can't find booking with id :{id}");
                return;
            }
            if (existingBooking.CheckIn <= DateTime.Today )
            {
                Console.Clear();
                Console.WriteLine("You can only delete a booking that haven't already started.");
                return;
            }
            if (!(existingBooking == null && existingBooking.CheckIn <= DateTime.Today))
            {
                existingBooking.IsActive = false;
                _dbContext.SaveChanges();
            }
        }

        public List<Booking> GetAllBookings()
        {
            return _dbContext.Bookings
                .Include(b => b.Guest)
                .Include(b => b.Invoice)
                .Include(b => b.Room)
                    .ThenInclude(r => r.RoomType)
                    .OrderBy(b => b.CheckIn)
                .ToList();
        }

        public Booking? GetBookingById(int id)
        {
            return _dbContext.Bookings.FirstOrDefault(b => b.Id == id);
        }

        public void UpdateBooking(Booking booking, Booking bookingToUpdate)
        {
            bookingToUpdate.CheckIn = booking.CheckIn;
            bookingToUpdate.CheckOut = booking.CheckOut;
            bookingToUpdate.NumberOfGuests  = booking.NumberOfGuests;
            bookingToUpdate.Room = booking.Room;
            bookingToUpdate.IsActive = booking.IsActive;
            bookingToUpdate.Invoice = booking.Invoice;
            _dbContext.SaveChanges();
        }

        public List<Booking> GetAllActiveBookings()
        {
            return _dbContext.Bookings
               .Include(b => b.Guest)
               .Include(b => b.Invoice)
               .Include(b => b.Room)
                   .ThenInclude(r => r.RoomType)
                   .Where(b => b.IsActive == true)
                   .OrderBy(b => b.CheckIn)
               .ToList();
        }

        public List<Booking> GetAllDeletedBookings()
        {
            return _dbContext.Bookings
            .Include(b => b.Guest)
            .Include (b => b.Invoice)
            .Include(b => b.Room)
                .ThenInclude(r => r.RoomType)
            .Where(b => b.IsActive == false)
            .OrderBy(b => b.CheckIn)
            .ToList();
        }

        public List<Booking> GetAllUpcomingBookings()
        {
            return _dbContext.Bookings
            .Include(b => b.Guest)
            .Include(b => b.Invoice)
            .Include(b => b.Room)
                .ThenInclude(r => r.RoomType)
            .Where(b => b.CheckIn > DateTime.Today && b.IsActive == true)
            .OrderBy (b => b.CheckIn)
            .ToList();
        }
        public List<Room> GetAvailableRooms(DateTime checkIn, DateTime checkOut, int guests)
        {
            var bookedRoomIds = GetBookedRoomsId(checkIn, checkOut);
            
            return _dbContext.Rooms
            .Where(r => r.IsActive
                && (r.MaxGuestsPerRoomWithoutExtrabeds + r.ExtraBedsAvailable) >= guests
                && !bookedRoomIds.Contains(r.Id))
             .OrderBy(r => r.RoomNumber)
             .Include(r => r.RoomType)
            .ToList();
        }

        public List<Room> GetAvailableRoomsToUpdateBooking(DateTime checkIn, DateTime checkOut, int guests, int bookingid)
        {
            var bookedRoomIds = GetBookedRoomsId(checkIn, checkOut);
            var currentBookingRoomId = _dbContext.Bookings
            .Where(b => b.Id == bookingid)
            .Select(b => (int?)b.Room.Id)
            .FirstOrDefault();

            if (currentBookingRoomId.HasValue)
            {
                bookedRoomIds.RemoveAll(id => id == currentBookingRoomId.Value);
            }
            return _dbContext.Rooms
            .Where(r => r.IsActive
                && (r.MaxGuestsPerRoomWithoutExtrabeds + r.ExtraBedsAvailable) >= guests
                && !bookedRoomIds.Contains(r.Id))
             .OrderBy(r => r.RoomNumber)
             .Include(r => r.RoomType)
            .ToList();     
        }
        public List<int> GetBookedRoomsId(DateTime checkIn, DateTime checkOut)
        {
            return _dbContext.Bookings
            .Where(b => b.IsActive && b.CheckIn < checkOut && b.CheckOut > checkIn)
            .Select(b => b.Room.Id)
            .Distinct()
            .ToList();
        }

        public void DeleteBookingNotPayedInTime(List<Booking> allBookings)
        {
            foreach (var booking in allBookings)
            {
                if (booking.Invoice.LastDayToPay < DateTime.Now)
                {
                booking.IsActive = false;
                booking.Invoice.IsActive = false;
                }

            }
        }
    }
}
