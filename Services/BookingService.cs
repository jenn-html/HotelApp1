using HotelApp1.Models;
using HotelApp1.Repositories;
using HotelApp1.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Services
{
    public class BookingService : ICRUDBooking
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

        public Booking? GetBookingById(int id)
        {
            return _bookingRepository.GetBookingById(id);
        }

        public void DeleteBookingById(int id)
        {
            _bookingRepository.DeleteBookingById(id);
        }

        public void UpdateBooking(Booking booking, Booking bookingToUpdate)
        {
            _bookingRepository.UpdateBooking(booking, bookingToUpdate);
        }
        public void Add(Booking booking)
        {

            _bookingRepository.Add(booking);
        }

        public List<Booking> GetAllActiveBookings()
        {
            return _bookingRepository.GetAllActiveBookings();
        }

        public List<Booking> GetAllDeletedBookings()
        {
            return _bookingRepository.GetAllDeletedBookings();
        }

        public List<Booking> GetAllUpcomingBookings()
        {
            return _bookingRepository.GetAllUpcomingBookings();
        }

        public List<Room> GetAvailableRooms(DateTime checkIn, DateTime checkOut, int guests)
        {
            return _bookingRepository.GetAvailableRooms(checkIn, checkOut, guests);
        }

        public List<Room> GetAvailableRoomsToUpdateBooking(DateTime checkin, DateTime checkout, int guests, int bookingid)
        {
            return _bookingRepository.GetAvailableRoomsToUpdateBooking(checkin,checkout,guests, bookingid);
        }

        public void DeleteBookingNotPayedInTime(List<Booking> allBookings)
        {
            _bookingRepository.DeleteBookingNotPayedInTime(allBookings);
        }
    }
}
