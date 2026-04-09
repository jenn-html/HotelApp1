using HotelApp1.Models;
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
        private readonly RoomService _roomService;
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
            //Console.WriteLine("Enter check in date");
            //var checkInDate = Convert.ToDateTime(Console.ReadLine());
            //Console.WriteLine("Enter check out date");
            //var checkOutDate = Convert.ToDateTime(Console.ReadLine());
            //Console.WriteLine("Select roomnumber");
            //var selectedRoom = _roomService.
            //var booking = new Booking
            //{
            //    CheckIn = checkInDate,
            //    CheckOut = checkOutDate,
            //    Room = ,
            //    NumberOfGuests = numberOfGuests,
            //    Guest = guest


            //};
            //_bookingService.Add(booking);
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
                else
                {
                    Console.WriteLine();
                }
                // Updatera bokning

            }

            catch
            {
                AnsiConsole.WriteLine("ID was not in the right format");
                Console.WriteLine("Press enter and try again");
                Console.ReadKey();
            }

        }
        public void RemoveBooking()
        {
            { }
        }
    }
}