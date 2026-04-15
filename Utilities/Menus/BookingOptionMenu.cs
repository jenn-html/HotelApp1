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
        private readonly RoomController _roomController;
        public BookingOptionMenu(BookingController bookingController, ApplicationDbContext dbContext, GuestController guestController, RoomController roomController )
        {
            _dbContext = dbContext;
            _bookingController = bookingController;
            _guestController = guestController;
            _roomController = roomController;
        }
        public void ShowMenu()
        {
            var bookingAvailableMenu = new BookingAvailabilityCalendar( _bookingController);
            var bookingViewMenu = new BookingViewMenu(_bookingController);
            var id = 0;
            string[] options = new[] {
           
                "View bookings",
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
                        Console.WriteLine("Invalid option.");
                        break;
                }
                Console.WriteLine("Press any key to go back to the menu...");
                Console.ReadKey();
            }
        }
    }
}
