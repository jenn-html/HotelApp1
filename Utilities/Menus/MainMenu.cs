using System;
using System.Collections.Generic;
using System.Text;
using HotelApp1.Controllers;
using HotelApp1.Data;
using Spectre.Console;

namespace HotelApp1.Utilities.Menus
{
    public class MainMenu : IMenu
    {
        private readonly BookingController _bookingController;
        private readonly GuestController _guestController;
        private readonly RoomController _roomController;
        private readonly ApplicationDbContext _context;
        public MainMenu(BookingController bookingController, GuestController guestController, ApplicationDbContext context, RoomController roomController) 
        {
            _context = context;
            _bookingController = bookingController;
            _guestController = guestController;
            _roomController = roomController;
        }

        public void ShowMenu()
        {

            var booking = new BookingOptionMenu(_bookingController, _context, _guestController, _roomController);
            var guest = new GuestOptionMenu(_guestController);
            var room = new RoomOptionMenu(_roomController);

            string[] options = new[] {
                "BOOKING MENU",
                "GUEST MENU",
                "ROOM MENU",
                "EXIT"
            };
            
            while (true)
            {
                Console.Clear();

                Console.WriteLine(" \r\n _____                                                                                       _____ \r\n( ___ )                                                                                     ( ___ )\r\n |   |~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|   | \r\n |   |                                                                                       |   | \r\n |   |                                                                                       |   | \r\n |   | ██╗  ██╗ ██████╗ ████████╗███████╗██╗              ██╗███████╗███╗   ██╗███╗   ██╗██╗ |   | \r\n |   | ██║  ██║██╔═══██╗╚══██╔══╝██╔════╝██║              ██║██╔════╝████╗  ██║████╗  ██║██║ |   | \r\n |   | ███████║██║   ██║   ██║   █████╗  ██║              ██║█████╗  ██╔██╗ ██║██╔██╗ ██║██║ |   | \r\n |   | ██╔══██║██║   ██║   ██║   ██╔══╝  ██║         ██   ██║██╔══╝  ██║╚██╗██║██║╚██╗██║██║ |   | \r\n |   | ██║  ██║╚██████╔╝   ██║   ███████╗███████╗    ╚█████╔╝███████╗██║ ╚████║██║ ╚████║██║ |   | \r\n |   | ╚═╝  ╚═╝ ╚═════╝    ╚═╝   ╚══════╝╚══════╝     ╚════╝ ╚══════╝╚═╝  ╚═══╝╚═╝  ╚═══╝╚═╝ |   | \r\n |   |                                                                                       |   | \r\n |   |                                                                                       |   | \r\n |___|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|___| \r\n(_____)                                                                                     (_____)\r\n");

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
