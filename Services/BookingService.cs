using HotelApp1.Models;
using HotelApp1.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Services
{
    public class BookingService 
    {

        private readonly BookingRepository _bookingRepository;
        public BookingService(BookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public List<Booking> GetAllBookings()
        {
            return _bookingRepository.GetAllBookings();
        }

        public Booking? GetById(int id)
        {
            return _bookingRepository.GetById(id);
        }

        public void DeleteById(int id)
        {
            _bookingRepository.DeleteById(id);
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
        public void Add(Booking booking)
        {

            _bookingRepository.Add(booking);
        }
    }
}
