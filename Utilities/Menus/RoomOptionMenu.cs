using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Utilities.Menus
{
    public class RoomOptionMenu : IMenu
    {
        public void ShowMenu()
        {

            string[] options = new[] {
                "Show all rooms",
                "Add a new room",
                "Update a room",
                "DeleteById a room",
                "Go back to Main"
            };

            while (true)
            {
                Console.Clear();

                // Use Spectre.Console SelectionPrompt for keyboard navigation + Enter selection
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[MediumPurple3_1]---ROOM MENU---[/]")
                        .PageSize(5)
                        .AddChoices(options)
                        .WrapAround()
                );

                int selected = Array.IndexOf(options, choice);

                Console.Clear();
                switch (selected)
                {
                    case 0:
                        Console.WriteLine("Showing all rooms...");
                        // TODO: show bookings
                        break;
                    case 1:
                        Console.WriteLine("Adding a new room...");
                        // TODO: add booking
                        break;
                    case 2:
                        Console.WriteLine("Updating a room...");
                        // TODO: update booking
                        break;
                    case 3:
                        Console.WriteLine("Deleting a room...");
                        // TODO: delete booking
                        break;
                    case 4:
                        return;
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
