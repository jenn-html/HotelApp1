using HotelApp1.Models;
using HotelApp1.Repositories;
using HotelApp1.Services;
using HotelApp1.UI;
using HotelApp1.UI.Calendar;
using HotelApp1.UI.Prompts;
using Spectre.Console;

namespace HotelApp1.Controllers
{
    public class BookingController 
    {
        private readonly GuestController _guestController;
        private readonly BookingService _bookingService;
        private readonly RoomController _roomController;
        private readonly InvoiceController _invoiceController;

        public BookingController(BookingService bookingService, GuestController guestController, RoomController roomController, InvoiceController invoiceController)
        {
            _bookingService = bookingService;
            _guestController = guestController;
            _roomController = roomController;
            _invoiceController = invoiceController;
        }

        public void DeleteBookingNotPayedInTime()
        {
            var allBookings = _bookingService.GetAllActiveBookings();
            _bookingService.DeleteBookingNotPayedInTime(allBookings);
        }
        public void GetAllBookings()
        {
            DeleteBookingNotPayedInTime();
            var getAllBookings = _bookingService.GetAllBookings();
            AnsiConsole.MarkupLine("[SeaGreen1]---LIST OF ALL BOOKINGS---[/]");
            if (getAllBookings.Any())
            { 
                ShowBookings(getAllBookings);
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]No bookings found![/]");
                return;
            }

        }
        public void GetAllActiveBookings()
        {
            DeleteBookingNotPayedInTime();
               var activeBookings = _bookingService.GetAllActiveBookings();
            AnsiConsole.MarkupLine("[SeaGreen1]---LIST OF ACTIVE BOOKINGS---[/]");
            if (activeBookings.Any())
            {
            ShowBookings(activeBookings);
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]No active bookings found[/]");
                return;
            }

        }
        public void GetAllDeletedBookings()
        {
            DeleteBookingNotPayedInTime();
            var deletedBookings = _bookingService.GetAllDeletedBookings();
            AnsiConsole.MarkupLine("[SeaGreen1]---LIST OF DELETED BOOKINGS---[/]");
            if (deletedBookings.Any())
            {            
                ShowBookings(deletedBookings);

            }
            else
            {
                AnsiConsole.MarkupLine($"[red]No deleted bookings found[/]");
                return;
            }

        }
        public void GetAllUpcomingBookings()
        {
            DeleteBookingNotPayedInTime();
            var upcomingBookings = _bookingService.GetAllUpcomingBookings();
            AnsiConsole.MarkupLine("[SeaGreen1]---LIST OF UPCOMING BOOKINGS---[/]");
            if (upcomingBookings.Any())
            {
                ShowBookings(upcomingBookings);
                return;
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]No upcoming bookings found[/]");
                return;
            }
        }
        public void ShowBookings(List<Booking> bookings)
        {
            var table = new Table();
            table.Border = TableBorder.Double;
            table.ShowRowSeparators = true;

            table.AddColumn("Booking ID");
            table.AddColumn("Guest");
            table.AddColumn("Check-in date");
            table.AddColumn("Check-out date");
            table.AddColumn("Number of guests");
            table.AddColumn("Room number");
            table.AddColumn("Room type");
            table.AddColumn("Date of booking");
            table.AddColumn("Invoice ID");
            foreach (var booking in bookings)
            {
                table.AddRow(
                    $"{booking.Id}",
                    $"{booking.Guest.FirstName + " " + booking.Guest.LastName}, ID: {booking.Guest.Id}",
                    $"{booking.CheckIn}",
                    $"{booking.CheckOut}",
                    $"{booking.NumberOfGuests}",
                    $"{booking.Room.RoomNumber}",
                    $"{booking.Room.RoomType.Type}",
                    $"{booking.TimeOfBooking}",
                    $"{booking.Invoice.ID}");

            }
            AnsiConsole.Write(table);
        }

        public void CheckAndSelectDateForBooking(BookingController bookingController, int bookingId)
        {
            var bookingCalender = new BookingAvailabilityCalendar(bookingController);
            bookingCalender.ShowAvailability(bookingId);

        }
        public void SelectAndUpdateBooking()
        {
            GetAllUpcomingBookings();
            AnsiConsole.MarkupLine("Enter ID of booking to update");

            var id = AnsiConsole.Prompt(
                new TextPrompt<int>("[yellow]Booking ID:[/]")
                    .Validate(i => i > 0 ? ValidationResult.Success() : ValidationResult.Error("[red]Enter a positive integer[/]"))
            );
            var booking = _bookingService.GetBookingById(id);
            if (booking == null || booking.IsActive == false) { AnsiConsole.MarkupLine($"[red]Could not find booking with id {id}[/]"); }
            else
            {
                var bookingController = new BookingController(_bookingService, _guestController, _roomController, _invoiceController);
                CheckAndSelectDateForBooking(bookingController, id);
            }

        }
        public void UpdateBooking(DateTime checkIn, DateTime checkOut, int guests, int selectedRoomId, int bookingid)
        {
            var bookingToUpdate = _bookingService.GetBookingById(bookingid);
            var selectedRoom = _roomController.GetRoomById(selectedRoomId);
            var numberOfNights = (checkOut - checkIn).Days;
            var numberofmaxguests = selectedRoom.MaxGuestsPerRoomWithoutExtrabeds + selectedRoom.ExtraBedsAvailable;
            int numberOfExtrabeds = 0;
            if (guests > selectedRoom.MaxGuestsPerRoomWithoutExtrabeds)
            {
                numberOfExtrabeds = guests - selectedRoom.MaxGuestsPerRoomWithoutExtrabeds;
            }
            try
            {
                var description = _invoiceController.CreateDescription(selectedRoom, numberOfNights, numberOfExtrabeds);
                var newInvoice = new Invoice
                {
                    Created = DateTime.Now,
                    TotalAmount = (selectedRoom.PricePerNight * numberOfNights) + (selectedRoom.PriceExtraBeds * numberOfExtrabeds),
                    Description = description,
                    LastDayToPay = DateTime.Now.AddDays(10)

                };
                try
                {
                    _invoiceController.CreateInvoice(newInvoice);
                    var booking = new Booking
                    {
                        Guest = bookingToUpdate.Guest,
                        CheckIn = checkIn,
                        CheckOut = checkOut,
                        NumberOfGuests = guests,
                        Room = selectedRoom,
                        IsActive = true,
                        Invoice = newInvoice
                    };
                    try
                    {
                        _bookingService.UpdateBooking(booking, bookingToUpdate);
                        _invoiceController.DeleteInvoice(bookingToUpdate);
                        AnsiConsole.MarkupLine("[green]Booking successfully updated![/]");
                    }

                    catch
                    {
                        AnsiConsole.MarkupLine("[red]Could not update booking[/]");
                    }
                }
                catch
                {
                    AnsiConsole.MarkupLine("[red] Unable to create invoice");
                }
            }
            catch
            {
                AnsiConsole.MarkupLine($"[red]Something went wrong when trying to create description for invoice[/]");
            }
        }

        public void RemoveBooking()
        {
            bool run = true;
            while (run)
            {
                 GetAllUpcomingBookings();
               
                AnsiConsole.WriteLine("Enter ID of booking to delete");
                try
                {
                    var bookingIdToDelete = BookingPrompts.PromptEnterId();
                    var bookingToDelete = _bookingService.GetBookingById(bookingIdToDelete);
                    var bookings = _bookingService.GetAllDeletedBookings();
                    var isDeleted = false;
                    foreach (var booking in bookings)
                    {
                        if (booking.Id == bookingToDelete.Id)
                        {
                            isDeleted = true;
                        }
                    }
                    if (bookingToDelete != null && isDeleted == false)
                    {
                        _bookingService.DeleteBookingById(bookingIdToDelete);
                        _invoiceController.DeleteInvoice(bookingToDelete);
                        AnsiConsole.MarkupLine($"[green]Booking successfully deleted![/]");
                        run = false;
                        return;
                    }
                    else
                    {
                        AnsiConsole.MarkupLine($"[red]No booking with id {bookingIdToDelete} found[/]");
                        if (AnsiConsole.Confirm("Try again?"))
                        {
                            Console.Clear();
                        }
                        else
                        {
                            run = false;
                            return;
                        }
                    }

                }
                catch
                {
                    Console.WriteLine("Wrong guestId format");
                }
            }
        }

        public List<Room> GetAvailibleRooms(DateTime checkIn, DateTime checkOut, int guests)
        {
            return _bookingService.GetAvailableRooms(checkIn, checkOut, guests);
        }
        public List<Room> GetAvailableRoomsToUpdateBooking(DateTime checkin, DateTime checkout, int guests, int bookingId)
        {
            return _bookingService.GetAvailableRoomsToUpdateBooking(checkin, checkout, guests, bookingId);
        }

        public void AddNewBooking(DateTime checkIn, DateTime checkOut, int guests, int roomId)
        {
            Guest guest = null;
            Room room = null;
            var run = true;

            while (run)
            {

                AnsiConsole.MarkupLine("Add a new guest or choose from list");
                if (AnsiConsole.Confirm("Add a new guest?"))
                {
                    _guestController.AddNewGuest();
                    Console.Clear(); 
                    _guestController.GetAllActiveGuests();
                }
                else
                {
                    Console.Clear();
                    _guestController.GetAllActiveGuests();
                }
                try
                {
                    while (run)
                    {
                        var guestId = BookingPrompts.PromptEnterId();
                        guest = _guestController.GetById(guestId);
                        if (guest == null)
                        {
                            AnsiConsole.MarkupLine($"[red]Guest with id {guestId} not found[/]");
                            if (AnsiConsole.Confirm("Try again?"))
                            {
                                AnsiConsole.WriteLine("Enter another guest id");
                            }
                            else
                            {
                                run = false;
                                break;
                            }
                        }
                        else
                        {
                            try
                            {

                                room = _roomController.GetRoomById(roomId);
                                if (room == null)
                                {
                                    AnsiConsole.MarkupLine($"{roomId} not found.");

                                }
                                run = false;
                                    break;
                            }
                            catch
                            {
                                AnsiConsole.MarkupLine($"[red]Something went wrong trying to find room[/]");
                            }
                        }
                    }
                    var numberOfNights = (checkOut - checkIn).Days;
                    var numberofmaxguests = room.MaxGuestsPerRoomWithoutExtrabeds + room.ExtraBedsAvailable;
                    int numberOfExtrabeds = 0;
                    if (guests > room.MaxGuestsPerRoomWithoutExtrabeds)
                    {
                        numberOfExtrabeds = guests - room.MaxGuestsPerRoomWithoutExtrabeds;
                    }
                    try
                    {
                        var description = _invoiceController.CreateDescription(room, numberOfNights, numberOfExtrabeds);
                        var totalAmount = (room.PricePerNight * numberOfNights) + (room.PriceExtraBeds * numberOfExtrabeds);
                        var newInvoice = new Invoice
                        {
                            Created = DateTime.Now,
                            TotalAmount = totalAmount,
                            Description = description,
                            IsPayed = false,
                            IsActive = true,
                            LastDayToPay = DateTime.Now.AddDays(10)
                        };
                        try
                        {
                            var invoice = _invoiceController.CreateInvoice(newInvoice);
                            var newBooking = new Booking
                            {
                                Guest = guest,
                                Room = room,
                                NumberOfGuests = 2,
                                CheckIn = checkIn,
                                CheckOut = checkOut,
                                Invoice = invoice
                            }; try
                            {
                                _bookingService.Add(newBooking);
                                AnsiConsole.MarkupLine($"[green]Booking added![/]");
                            }
                            catch
                            {
                                AnsiConsole.MarkupLine($"[red]Could not create booking[/]");
                            }
                        }
                        catch
                        {
                            AnsiConsole.MarkupLine($"[red] Unable to create invoice[/]");
                        }
                    }
                    catch
                    {
                        AnsiConsole.MarkupLine($"[red]Something went wrong when trying to create description for invoice[/]");
                    }
                }
                catch
                {
                    AnsiConsole.MarkupLine($"[red]Something went wrong when trying to get guest[/]");
                }
                    run = false;
                    break;
            }
        }

    }
}