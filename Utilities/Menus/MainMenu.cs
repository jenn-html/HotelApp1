using System;
using System.Collections.Generic;
using System.Text;
using HotelApp1.Controllers;
using Spectre.Console;

namespace HotelApp1.Utilities.Menus
{
    public class MainMenu : IMenu
    {
        private readonly BookingController _bookingController;
        private readonly GuestController _guestController;
        public MainMenu(BookingController bookingController, GuestController guestController) 
        {
            _bookingController = bookingController;
            _guestController = guestController;
        }

        public void ShowMenu()
        {
            var booking = new BookingOptionMenu(_bookingController);
            var guest = new GuestOptionMenu();
            var room = new RoomOptionMenu();

            string[] options = new[] {
                "BOOKING MENU",
                "GUEST MENU",
                "ROOM MENU",
                "EXIT"
            };
            
            while (true)
            {
                Console.Clear();

                // Use Spectre.Console SelectionPrompt for keyboard navigation + Enter selection
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[DeepSkyBlue2]---HOTEL BOOKING SYSTEM---[/]")
                        .PageSize(4)
                        .AddChoices(options)
                        .WrapAround()
                );

                int selected = Array.IndexOf(options, choice);

                Console.Clear();
                switch (selected)
                {
                    case 0:
                        booking.ShowMenu();
                        
                        break;
                    case 1:
                        guest.ShowMenu();
                        
                        break;
                    case 2:
                        room.ShowMenu();
                        
                        break;
                    case 3:
                        Console.WriteLine("Exiting...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey(true);
            }
        }
    }
}
