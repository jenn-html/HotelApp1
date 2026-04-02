using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Utilities.Menus
{
    public class BookingOptionMenu : IMenu
    {
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
                        Console.WriteLine("Showing all bookings...");
                        // TODO: show bookings
                        break;
                    case 1:
                        Console.WriteLine("Adding a new booking...");
                        // TODO: add booking
                        break;
                    case 2:
                        Console.WriteLine("Updating a booking...");
                        // TODO: update booking
                        break;
                    case 3:
                        Console.WriteLine("Deleting a booking...");
                        // TODO: delete booking
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
