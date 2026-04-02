using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Utilities.Menus
{
    public class GuestOptionMenu : IMenu
    {
        public void ShowMenu()
        {

            string[] options = new[] {
                "Show all guests",
                "Add a new guest",
                "Update a guest",
                "Delete a guest",
                "Go back to Main"
            };
            var displayMenu = true;
            while (displayMenu = true)
            {
                Console.Clear();

                // Use Spectre.Console SelectionPrompt for keyboard navigation + Enter selection
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[Orchid]---GUEST MENU---[/]")
                        .PageSize(5)
                        .AddChoices(options)
                        .WrapAround()
                );

                int selected = Array.IndexOf(options, choice);

                Console.Clear();
                switch (selected)
                {
                    case 0:
                        Console.WriteLine("Showing all guests...");
                        // TODO: show bookings
                        break;
                    case 1:
                        Console.WriteLine("Adding a new guest...");
                        // TODO: add booking
                        break;
                    case 2:
                        Console.WriteLine("Updating a guest...");
                        // TODO: update booking
                        break;
                    case 3:
                        Console.WriteLine("Deleting a guest...");
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
