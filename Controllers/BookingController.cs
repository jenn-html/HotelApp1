using HotelApp1.Services;
using Spectre.Console;
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
                Console.WriteLine("---------------------------");
            }
        }
        public void AddBooking()//Lägg till bokning
        {
            Console.WriteLine("Add booking..");
        }
        public void UpdateBooking()
        {
            ShowAllBookings();
            AnsiConsole.WriteLine("Enter ID of booking to update or BACK to go back");
            AnsiConsole.WriteLine("---------------------------");
            try
            {
                var inputId = Console.ReadLine();
                if (inputId == "back" || inputId == "BACK")
                {
                    return;
                }
                var id = Convert.ToInt32(inputId);
            var existingBooking = _bookingService.GetById(id);
                if (existingBooking == null)
                {
                    Console.WriteLine("Booking not found");
                    return;
                }

            }

            catch 
            {
                AnsiConsole.WriteLine("ID was not in the right format");
                Console.WriteLine("Press enter and try again");
                Console.ReadKey();
            }

        }
        public void RemoveBooking() {
            { }
    }
}
