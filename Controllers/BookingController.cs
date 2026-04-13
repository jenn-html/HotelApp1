using HotelApp1.Data;
using HotelApp1.Models;
using HotelApp1.Services;
using HotelApp1.UI;
using HotelApp1.Utilities.Menus;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Controllers
{
    public class BookingController
    {
        public readonly GuestService _guestService;
        private readonly BookingService _bookingService;
        //private readonly RoomService _roomService;
        public BookingController(BookingService bookingService, GuestService guestService)
        {
            _bookingService = bookingService;
            _guestService = guestService;
        }

        public void ShowAllBookings()
        {
            var bookings = _bookingService.GetAllBookings();
            Console.WriteLine("\n List of bookings");
            var table = new Table();
            table.Border = TableBorder.Double;
            table.ShowRowSeparators = true;

            table.AddColumn("Booking ID");
            table.AddColumn("Name");
            table.AddColumn("Check-in date");
            table.AddColumn("Check-out date");
            table.AddColumn("Number of guests");
            table.AddColumn("Room number");
            table.AddColumn("Room type");
            table.AddColumn("Date of booking");
            foreach (var booking in bookings)
            {
                table.AddRow($"{booking.Id}", $"{booking.Guest.FirstName + " " + booking.Guest.LastName}", $"{booking.CheckIn}", $"{booking.CheckOut}", $"{booking.NumberOfGuests}", $"{booking.Room.RoomNumber}", $"{booking.Room.RoomType}", $"{booking.TimeOfBooking}");
                //Console.WriteLine($"Booking ID: {booking.Id}, Name: {booking.Guest.FirstName} {booking.Guest.LastName}, Check-in date: {booking.CheckIn}, Check-out date: {booking.CheckOut}," +
                //    $"Number of guests: {booking.NumberOfGuests}, Room number: {booking.Room.RoomNumber}, Room type: {booking.Room.RoomType.Type}, Date of booking: {booking.TimeOfBooking}");
                //Console.WriteLine("---------------------------");
            }

            AnsiConsole.Write(table);
        }
        public void AddBooking(ApplicationDbContext dbContext, DateTime checkin, DateTime chechout, int guests, int selectedroom)//Lägg till bokning
        {
            _guestService.GetAllGuests();
            Console.WriteLine("Select guest by ID");
            try
            {
                var id = Convert.ToInt32(Console.ReadLine());
                var guest = _guestService.GetById(id);
                var room = dbContext.Rooms.SingleOrDefault(r => r.RoomNumber == selectedroom);

                var newBooking = new Booking
                {
                    Guest = guest,
                    Room = room,
                    NumberOfGuests = 2,
                    CheckIn = checkin,
                    CheckOut = chechout
                };
                _bookingService.Add(newBooking);
                dbContext.SaveChanges();
            }
            catch 
            {
                Console.WriteLine("Wrong Guest id, try again");
            }
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
                    Console.WriteLine("Update name:");
                    Console.WriteLine("Update ");
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
            {
                ShowAllBookings();
                AnsiConsole.WriteLine("Enter ID of booking to delete");
                try
                {
                    var bookingToDelete = Convert.ToInt32(Console.ReadLine());

                    _bookingService.DeleteById(bookingToDelete);
                }
                catch 
                {
                    Console.WriteLine("Wrong id format");
                }
            }
        }
    }
}