using HotelApp1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Utilities.Interfaces
{
    public interface ICRUDBooking
    {
        List<Booking> GetAllBookings();
        Booking? GetBookingById(int id);
        void Add(Booking booking);
        void UpdateBooking(Booking booking, Booking booking1);
        void DeleteBookingById(int id);
        public List<Booking> GetAllActiveBookings();

        public List<Booking> GetAllDeletedBookings();
        public List<Booking> GetAllUpcomingBookings();
        public List<Room> GetAvailableRooms(DateTime checkIn, DateTime checkOut, int guests);
        public List<Room> GetAvailableRoomsToUpdateBooking(DateTime checkIn, DateTime checkOut, int guests, int bookingid);
    }
}
