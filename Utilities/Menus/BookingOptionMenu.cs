using HotelApp1.Controllers;
using HotelApp1.Data;
using HotelApp1.UI;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Utilities.Menus
{
    public class BookingOptionMenu : IMenu
    {
        private readonly BookingController _bookingController;
        private readonly GuestController _guestController;
        private readonly ApplicationDbContext _dbContext;
        private readonly InvoiceController _invoiceController;
        private readonly RoomController _roomController;
        public BookingOptionMenu(BookingController bookingController, ApplicationDbContext dbContext, GuestController guestController, RoomController roomController, InvoiceController invoiceController   )
        {
            _dbContext = dbContext;
            _bookingController = bookingController;
            _guestController = guestController;
            _roomController = roomController;
            _invoiceController = invoiceController;
        }
        public void ShowMenu()
        {
            var bookingAvailableMenu = new BookingAvailabilityCalendar( _bookingController);
            var bookingViewMenu = new BookingViewMenu(_bookingController);
            var id = 0;
            string[] options = new[] {

                $"[LightGreen_1]VIEW BOOKINGS[/]",
                $"[LightGreen_1]ADD A NEW BOOKING[/]",
                $"[LightGreen_1]UPDATE A BOOKING[/]",
                $"[LightGreen_1]DELETE A BOOKING[/]",
                $"[yellow]GO BACK TO MAIN[/]"
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
                        bookingViewMenu.ShowMenu();
                        break;
                    case 1:
                        //bookingAvailableMenu.ShowAvailability();
                        _bookingController.CheckAndSelectDateForBooking(_bookingController, id);
                        
                        break;
                    case 2:
                        _bookingController.SelectAndUpdateBooking();
                        break;
                    case 3:
                        _bookingController.RemoveBooking();
                        break;
                    case 4: 
                        return;
                    default:
                        AnsiConsole.MarkupLine($"[red]Invalid option.[/]");
                        break;
                }
                Console.WriteLine("Press any key to go back to the menu...");
                Console.ReadKey();
            }
        }
    }
}
