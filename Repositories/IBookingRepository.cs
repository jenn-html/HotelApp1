using HotelApp1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Repositories
{
    public interface IBookingRepository
    {
        List<Booking> GetAllBookings();
        Booking? GetById(int id);
        void Add(Booking booking);
        void Update(Booking booking);
        void Delete(int id);
    }
}
