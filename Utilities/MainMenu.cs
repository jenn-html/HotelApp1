using System;
using System.Collections.Generic;
using System.Text;
using Spectre.Console;

namespace HotelApp1.Utilities
{
    internal class MainMenu
    {
        public static void ShowMenu()
        {
            string[] options = new[] {
                "Add a new room",
                "Update a room",
                "Delete a room",
                "Show all rooms",
                "Show room details",
                "Show all bookings",
                "Add a new booking",
                "Update a booking",
                "Delete a booking",
                "Exit"
            };

            while (true)
            {
                Console.Clear();

                // Use Spectre.Console SelectionPrompt for keyboard navigation + Enter selection
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[DeepSkyBlue2]---HOTEL BOOKING SYSTEM---[/]")
                        .PageSize(10)
                        .AddChoices(options)
                        .WrapAround()
                );

                int selected = Array.IndexOf(options, choice);

                Console.Clear();
                switch (selected)
                {
                    case 0:
                        Console.WriteLine("Adding a new room...");
                        // TODO: call add room flow
                        break;
                    case 1:
                        Console.WriteLine("Updating a room...");
                        // TODO: call update room flow
                        break;
                    case 2:
                        Console.WriteLine("Deleting a room...");
                        // TODO: call delete room flow
                        break;
                    case 3:
                        Console.WriteLine("Showing all rooms...");
                        // TODO: show rooms
                        break;
                    case 4:
                        Console.WriteLine("Showing room details...");
                        // TODO: show details
                        break;
                    case 5:
                        Console.WriteLine("Showing all bookings...");
                        // TODO: show bookings
                        break;
                    case 6:
                        Console.WriteLine("Adding a new booking...");
                        // TODO: add booking
                        break;
                    case 7:
                        Console.WriteLine("Updating a booking...");
                        // TODO: update booking
                        break;
                    case 8:
                        Console.WriteLine("Deleting a booking...");
                        // TODO: delete booking
                        break;
                    case 9:
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
