using HotelApp1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Repositories
{
    public interface IBookingRepository
    {
        List<Booking> GetAllBookings();
        Booking? GetBookingById(int id);
        void Add(Booking booking);
        void UpdateBooking(Booking booking, Booking booking1);
        void DeleteBookingById(int id);
    }
}
