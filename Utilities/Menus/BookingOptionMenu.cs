using HotelApp1.Controllers;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Utilities.Menus
{
    public class BookingOptionMenu : IMenu
    {
        private readonly BookingController _bookingController;
        public BookingOptionMenu(BookingController bookingController)
        {
            _bookingController = bookingController;
        }
        public void ShowMenu()
        {
            string[] options = new[] {
           
                "Show all bookings",
                "Add a new booking",
                "Update a booking",
                "Delete a booking",
                "Go back to Main"
            };
          
            while (true)
            {
                Console.Clear();

                // Use Spectre.Console SelectionPrompt for keyboard navigation + Enter selection
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[LightGreen_1]---BOOKING MENU---[/]")
                        .PageSize(5)
                        .AddChoices(options)
                        .WrapAround()
                );

                int selected = Array.IndexOf(options, choice);

                Console.Clear();
                switch (selected)
                {
                    
                    case 0:
                        _bookingController.ShowAllBookings();
                        break;
                    case 1:
                        _bookingController.AddBooking();
                        break;
                    case 2:
                        _bookingController.UpdateBooking();
                        break;
                    case 3:
                        _bookingController.RemoveBooking();
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
