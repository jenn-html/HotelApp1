using HotelApp1.Controllers;
using HotelApp1.Data;
using HotelApp1.UI;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Utilities.Menus
{
    public class BookingViewMenu : IMenu
    {
        private readonly BookingController _bookingController;

        public BookingViewMenu(BookingController bookingController)
        {
            _bookingController = bookingController;
        }
        public void ShowMenu()
        {
           
            string[] options = new[] {

                "All bookings",
                "Active bookings",
                "Upcoming bookings",
                "Deleted bookings",
                "Go back to Booking menu"
            };

            while (true)
            {
                Console.Clear();

                // Use Spectre.Console SelectionPrompt for keyboard navigation + Enter selection
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[SeaGreen1]---VIEW BOOKINGS MENU---[/]")
                        .PageSize(5)
                        .AddChoices(options)
                        .WrapAround()
                );

                int selected = Array.IndexOf(options, choice);

                Console.Clear();
                switch (selected)
                {

                    case 0:
                        _bookingController.GetAllBookings();
                        break;
                    case 1:
                        _bookingController.GetAllActiveBookings();
                        break;
                    case 2:
                        _bookingController.GetAllUpcomingBookings();
                        break;
                    case 3:
                        _bookingController.GetAllDeletedBookings();
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
                Console.WriteLine("Press any key to go back to the menu...");
                Console.ReadKey();
            }
        }
    }
}
