using System;
using System.Collections.Generic;
using System.Text;

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

            int selected = 0;
            ConsoleKey key;

            // Menu input loop
            do
            {
                Console.Clear();
                Console.WriteLine("---HOTEL BOOKING SYSTEM---");
                Console.WriteLine("Use Up/Down arrows to navigate and Enter to select\n");

                // render options
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selected)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($"> {i + 1}. {options[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {i + 1}. {options[i]}");
                    }
                }

                var keyInfo = Console.ReadKey(true);
                key = keyInfo.Key;

                if (key == ConsoleKey.UpArrow)
                {
                    selected = (selected <= 0) ? options.Length - 1 : selected - 1;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    selected = (selected >= options.Length - 1) ? 0 : selected + 1;
                }
                else if (char.IsDigit(keyInfo.KeyChar))
                {
                    // allow number keys as shortcut (1..9,0 for 10)
                    int num = (int)char.GetNumericValue(keyInfo.KeyChar);
                    if (num == 0 && options.Length >= 10)
                        num = 10;
                    if (num >= 1 && num <= options.Length)
                        selected = num - 1;
                }

            } while (key != ConsoleKey.Enter);

            // handle selection
            Console.Clear();
            switch (selected)
            {
                case 0:
                    Console.WriteLine("Adding a new room...");
                    // TODO: call add customer flow
                    break;
                case 1:
                    Console.WriteLine("Updating a room...");
                    // TODO: call update customer flow
                    break;
                case 2:
                    Console.WriteLine("Deleting a room...");
                    // TODO: call delete customer flow
                    break;
                case 3:
                    Console.WriteLine("Showing all rooms...");
                    // TODO: show customers
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
            // show menu again
            ShowMenu();
        }
    }
}
