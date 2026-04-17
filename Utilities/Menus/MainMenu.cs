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
        private readonly InvoiceController _invoiceController;
        private readonly ApplicationDbContext _context;
        public MainMenu(BookingController bookingController, GuestController guestController, ApplicationDbContext context, RoomController roomController, InvoiceController invoiceController) 
        {
            _context = context;
            _bookingController = bookingController;
            _guestController = guestController;
            _roomController = roomController;
            _invoiceController = invoiceController;
        }

        public void ShowMenu()
        {

            var booking = new BookingOptionMenu(_bookingController, _context, _guestController, _roomController, _invoiceController);
            var guest = new GuestOptionMenu(_guestController);
            var room = new RoomOptionMenu(_roomController);
            var invoice = new InvoiceOptionMenu(_invoiceController);

            string[] options = new[] {
                "[DeepPink2]BOOKING MENU[/]",
                "[DeepPink2]GUEST MENU[/]",
                "[DeepPink2]ROOM MENU[/]",
                "[DeepPink2]INVOICE MENU[/]",
                "[DeepPink2]EXIT[/]"
            };
            
            while (true)
            {
                Console.Clear();

                Console.WriteLine(" \r\n _____                                                                                       _____ \r\n( ___ )                                                                                     ( ___ )\r\n |   |~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|   | \r\n |   |                                                                                       |   | \r\n |   |                                                                                       |   | \r\n |   | ██╗  ██╗ ██████╗ ████████╗███████╗██╗              ██╗███████╗███╗   ██╗███╗   ██╗██╗ |   | \r\n |   | ██║  ██║██╔═══██╗╚══██╔══╝██╔════╝██║              ██║██╔════╝████╗  ██║████╗  ██║██║ |   | \r\n |   | ███████║██║   ██║   ██║   █████╗  ██║              ██║█████╗  ██╔██╗ ██║██╔██╗ ██║██║ |   | \r\n |   | ██╔══██║██║   ██║   ██║   ██╔══╝  ██║         ██   ██║██╔══╝  ██║╚██╗██║██║╚██╗██║██║ |   | \r\n |   | ██║  ██║╚██████╔╝   ██║   ███████╗███████╗    ╚█████╔╝███████╗██║ ╚████║██║ ╚████║██║ |   | \r\n |   | ╚═╝  ╚═╝ ╚═════╝    ╚═╝   ╚══════╝╚══════╝     ╚════╝ ╚══════╝╚═╝  ╚═══╝╚═╝  ╚═══╝╚═╝ |   | \r\n |   |                                                                                       |   | \r\n |   |                                                                                       |   | \r\n |___|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|___| \r\n(_____)                                                                                     (_____)\r\n");

                // Use Spectre.Console SelectionPrompt for keyboard navigation + Enter selection
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[DeepPink2]---HOTEL BOOKING SYSTEM---[/]")
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
                        
                        break;
                    case 1:
                        guest.ShowMenu();
                        
                        break;
                    case 2:
                        room.ShowMenu(_bookingController);
                        
                        break;
                    case 3:
                            invoice.ShowMenu();
                        break;
                    case 4:
                        AnsiConsole.MarkupLine($"[green]Exiting...[/]");
                        Environment.Exit(0);
                        break;
                    default:
                        AnsiConsole.MarkupLine($"[red]Invalid option.[/]");
                        break;
                }

                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey(true);
            }
        }
    }
}
