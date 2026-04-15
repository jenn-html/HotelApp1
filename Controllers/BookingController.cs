using HotelApp1.Data;
using HotelApp1.Models;
using HotelApp1.Services;
using HotelApp1.UI;
using HotelApp1.Utilities.Menus;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Controllers
{
    public class BookingController
    {
        private readonly GuestController _guestController;
        private readonly BookingService _bookingService;
        private readonly RoomController _roomController;
        //private readonly RoomService _roomService;
        public BookingController(BookingService bookingService, GuestController guestController, RoomController roomController)
        {
            _bookingService = bookingService;
            _guestController = guestController;
            _roomController = roomController;

        }

        public void GetAllBookings()
        {
            var getAllBookings = _bookingService.GetAllBookings();
            AnsiConsole.MarkupLine("[SeaGreen1]--LIST OF ALL BOOKINGS--[/]");
            ShowBookings(getAllBookings);
        }
        public void GetAllActiveBookings()
        {
            var activeBookings = _bookingService.GetAllActicveBookings();
            AnsiConsole.MarkupLine("[SeaGreen1]--LIST OF ACTIVE BOOKINGS--[/]");
            ShowBookings(activeBookings);
        }
        public void GetAllDeletedBookings()
        {
            var deletedBookings = _bookingService.GetAllDeletedBookings();
            AnsiConsole.MarkupLine("[SeaGreen1]--LIST OF DELETED BOOKINGS--[/]");
            ShowBookings(deletedBookings);
        }
        public void GetAllUpcomingBookings()
        {
            var upcomingBookings = _bookingService.GetAllUpcomingBookings();
            AnsiConsole.MarkupLine("[SeaGreen1]--LIST OF UPCOMING BOOKINGS--[/]");
            ShowBookings(upcomingBookings);
        }
        public void ShowBookings(List<Booking> bookings)
        {
            var table = new Table();
            table.Border = TableBorder.Double;
            table.ShowRowSeparators = true;

            table.AddColumn("Booking ID");
            table.AddColumn("Name");
            table.AddColumn("Check-in date");
            table.AddColumn("Check-out date");
            table.AddColumn("Number of guests");
            table.AddColumn("Room number");
            table.AddColumn("Room type");
            table.AddColumn("Date of booking");
            foreach (var booking in bookings)
            {
                table.AddRow($"{booking.Id}", $"{booking.Guest.FirstName + " " + booking.Guest.LastName}", $"{booking.CheckIn}", $"{booking.CheckOut}", $"{booking.NumberOfGuests}", $"{booking.Room.RoomNumber}", $"{booking.Room.RoomType.Type}", $"{booking.TimeOfBooking}");
            }
            AnsiConsole.Write(table);
        }

        public void CheckAndSelectDateForBooking(BookingController bookingController, int bookingId)//ApplicationDbContext dbContext, DateTime checkin, DateTime checkout, int guests, int selectedroom
        {
            var bookingCalender = new BookingAvailabilityCalendar(bookingController);
            bookingCalender.ShowAvailability(bookingId);
           
        }
        public void SelectAndUpdateBooking()
        {
            GetAllUpcomingBookings();
            AnsiConsole.MarkupLine("Enter ID of booking to update");
            AnsiConsole.MarkupLine("---------------------------");

            var id = AnsiConsole.Prompt(
                new TextPrompt<int>("[yellow]Booking ID:[/]")
                    .Validate(i => i > 0 ? ValidationResult.Success() : ValidationResult.Error("[red]Enter a positive integer[/]"))
            );
            var booking = _bookingService.GetById(id);
            if (booking == null) { AnsiConsole.WriteLine($"Could not find booking with id {id}"); }
            else
            {
                var bookingController = new BookingController(_bookingService, _guestController, _roomController);
                CheckAndSelectDateForBooking(bookingController, id);
            }

        }
        public void UpdateBooking(DateTime checkIn, DateTime checkOut, int guests, int selectedRoomId, int bookingid)
        {
            var bookingToUpdate = _bookingService.GetById(bookingid);
            var selectedRoom = _roomController.GetById(selectedRoomId);
            var booking = new Booking
            {
                Guest = bookingToUpdate.Guest,
                CheckIn = checkIn,
                CheckOut = checkOut,
                NumberOfGuests = guests,
                Room = selectedRoom,
                IsActive = true,
                IsDeleted = false
            };
            try
            {
                _bookingService.Update(booking, bookingToUpdate);
                AnsiConsole.MarkupLine("[green]Booking successfully updated![/]");
            }
            catch {
                AnsiConsole.MarkupLine("[red]Could not update booking[/]");
            }


        }
        //{
        //    GetAllBookings();
        //    AnsiConsole.MarkupLine("Enter ID of booking to update");
        //    AnsiConsole.MarkupLine("---------------------------");

        //    var guestId = AnsiConsole.Prompt(
        //        new TextPrompt<int>("[yellow]Booking ID:[/]")
        //            .Validate(i => i > 0 ? ValidationResult.Success() : ValidationResult.Error("[red]Enter a positive integer[/]"))
        //    );

        //    var existingBooking = _bookingService.GetById(guestId);
        //    if (existingBooking == null)
        //    {
        //        AnsiConsole.MarkupLine("[red]Booking not found[/]");
        //        return;
        //    }
        //    if (AnsiConsole.Confirm("Change check-in date?"))
        //    {
        //        var newCheckIn = AnsiConsole.Prompt(
        //            new TextPrompt<DateTime>("[yellow]Enter new check-in (yyyy-MM-dd):[/]")
        //                .Validate(d => d.Date >= DateTime.Today ? ValidationResult.Success() : ValidationResult.Error("[red]Check-in cannot be in the past[/]"))
        //        );
        //        existingBooking.CheckIn = newCheckIn.Date;
        //    }
        //    if (AnsiConsole.Confirm("Change check-out date?"))
        //    {
        //        var newCheckOut = AnsiConsole.Prompt(
        //            new TextPrompt<DateTime>("[yellow]Enter new check-out (yyyy-MM-dd):[/]")
        //                .Validate(d => d.Date > existingBooking.CheckIn.Date ? ValidationResult.Success() : ValidationResult.Error("[red]Check-out must be after check-in[/]"))
        //        );

        //        existingBooking.CheckOut = newCheckOut.Date;
        //    }
        //    if (AnsiConsole.Confirm("Change number of guests?"))
        //    {
        //        var newGuests = AnsiConsole.Prompt(
        //            new TextPrompt<int>("[yellow]Enter number of guests:[/]")
        //                .Validate(g => g > 0 && g < 5 ? ValidationResult.Success() : ValidationResult.Error("[red]Enter a number greater than zero[/]"))
        //        );
        //        existingBooking.NumberOfGuests = newGuests;
        //    }
        //    AnsiConsole.MarkupLine("\n[green]Updated booking summary:[/]");
        //    AnsiConsole.MarkupLine($"ID: [blue]{existingBooking.Id}[/]");
        //    AnsiConsole.MarkupLine($"Guest: [blue]{existingBooking.Guest?.FirstName} {existingBooking.Guest?.LastName}[/]");
        //    AnsiConsole.MarkupLine($"Room: [blue]{existingBooking.Room?.RoomNumber}[/]");
        //    AnsiConsole.MarkupLine($"Check-in: [blue]{existingBooking.CheckIn:yyyy-MM-dd}[/]");
        //    AnsiConsole.MarkupLine($"Check-out: [blue]{existingBooking.CheckOut:yyyy-MM-dd}[/]");
        //    AnsiConsole.MarkupLine($"Guests: [blue]{existingBooking.NumberOfGuests}[/]");

        //    if (AnsiConsole.Confirm("Save changes?"))
        //    {
        //        _bookingService.Update(existingBooking); 
        //        AnsiConsole.MarkupLine("[green]Booking updated successfully.[/]");
        //    }
        //    else
        //    {
        //        AnsiConsole.MarkupLine("[grey]Changes discarded.[/]");
        //    }
        //}

        public void RemoveBooking()
        {
            {
                GetAllUpcomingBookings();
                AnsiConsole.WriteLine("Enter ID of booking to delete");
                try
                {
                    var bookingToDelete = Convert.ToInt32(Console.ReadLine());

                    _bookingService.DeleteById(bookingToDelete);
                }
                catch 
                {
                    Console.WriteLine("Wrong guestId format");
                }
            }
        }

        public List<Room> GetAvailibleRooms(DateTime checkIn, DateTime checkOut, int guests)
        {
            return _bookingService.GetAvailibleRooms(checkIn, checkOut, guests);
        }
        public List<Room> GetAvailableRoomsToUpdateBooking (DateTime checkin , DateTime checkout, int guests, int bookingId)
        {
            return _bookingService.GetAvailibleRoomsToUpdateBooking(checkin , checkout, guests, bookingId);
        }

        public void AddNewBooking(DateTime checkIn, DateTime checkOut, int numberOfGuests, int roomId)
        {
            while (true)
            {
                _guestController.GetAllActiveGuests();
                AnsiConsole.WriteLine("Select guest by ID");
                try
                {
                    var guestId = Convert.ToInt32(Console.ReadLine());
                    var guest = _guestController.GetById(guestId);
                    if (guest == null)
                    {
                        AnsiConsole.MarkupLine($"[red]Guest with id {guestId} not found[/]");
                    }
                    else if (guest.IsDeleted == true)
                    {
                        AnsiConsole.MarkupLine($"[red]Guest with id {guestId} is not available[/]");  
                    }
                    else
                    {
                        var room = _roomController.GetById(roomId);

                        var newBooking = new Booking
                        {
                            Guest = guest,
                            Room = room,
                            NumberOfGuests = 2,
                            CheckIn = checkIn,
                            CheckOut = checkOut
                        };
                        _bookingService.Add(newBooking);

                        break;
                    }
                }
                catch
                {
                    Console.WriteLine("Wrong Guest guestId, try again");
                }
                if (AnsiConsole.Confirm("Try again?"))
                {
                    Console.Clear();
                }
                else { break; }
            }
        }
    }
}