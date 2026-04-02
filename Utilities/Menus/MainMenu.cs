using System;
using System.Collections.Generic;
using System.Text;
using Spectre.Console;

namespace HotelApp1.Utilities.Menus
{
    public class MainMenu : IMenu
    {

        public void ShowMenu()
        {
            var booking = new BookingOptionMenu();
            var guest = new GuestOptionMenu();
            var room = new RoomOptionMenu();

            string[] options = new[] {
                "BOOKING MENU",
                "GUEST MENU",
                "ROOM MENU",
                "INVOICE MENU",
                "EXIT"
            };
            
            while (true)
            {
                Console.Clear();

                // Use Spectre.Console SelectionPrompt for keyboard navigation + Enter selection
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[DeepSkyBlue2]---HOTEL BOOKING SYSTEM---[/]")
                        .PageSize(5)
                        .AddChoices(options)
                        .WrapAround()
                );

                int selected = Array.IndexOf(options, choice);

                Console.Clear();
                switch (selected)
                {
                    case 0:
                        booking.ShowMenu();
                        // Showing booking menu CRUD
                        break;
                    case 1:
                        guest.ShowMenu();
                        // Showing guest menu CRUD
                        break;
                    case 2:
                        room.ShowMenu();
                        // Showing room menu CRUD
                        break;
                    case 3:
                        Console.WriteLine("Invoices");
                        // TODO: Invoices VG uppgift
                        break;
                    case 4:
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
