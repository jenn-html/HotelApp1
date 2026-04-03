using HotelApp1.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Controllers
{
    public class BookingController
    {
        private readonly BookingService _bookingService;
        public BookingController(BookingService bookingService) 
        {
            _bookingService = bookingService;
        }

        public void ShowAllBookings()
        {
            var bookings = _bookingService.GetAllBookings();
            Console.WriteLine("\n List of bookings");
            foreach (var booking in bookings)
            {
                Console.WriteLine(booking);
                Console.WriteLine("---------------------------66");
            }
        }
    }
}
