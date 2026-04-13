using HotelApp1.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Controllers
{
    public class GuestController
    {
        public readonly GuestService _guestService;
        public GuestController(GuestService guestService)
        {
            _guestService = guestService;
        }
        public void AddNewGuest()
        {

        }
        public void ShowAllGuests()
        {
            var guests = _guestService.GetAllGuests();
            Console.WriteLine("\n List of guests");
            foreach (var guest in guests)
            {
                Console.WriteLine($"Guest ID: {guest.Id}, Name: {guest.FirstName} {guest.LastName}," +
                    $"Email: {guest.Email}, " +
                    $"Phone: {guest.Phone}, " +
                    $"Adress: {guest.Street}, {guest.PostalCode}, " +
                    $"{guest.City} {guest.Country}, " +
                    $"Bookings: {guest.Bookings.Count}");
                Console.WriteLine("---------------------------");
            }
        }
        public void UpdateGuest()
        {
            ShowAllGuests();
            AnsiConsole.WriteLine("Enter ID of Guest to update or BACK to go back");
            AnsiConsole.WriteLine("---------------------------");
            try
            {
                var inputId = Console.ReadLine();
                if (inputId == "back" || inputId == "BACK")
                {
                    return;
                }
                var id = Convert.ToInt32(inputId);
                var existingBooking = _guestService.GetById(id);
                if (existingBooking == null)
                {
                    Console.WriteLine("Guest not found");
                    return;
                }
                else
                {
                    Console.WriteLine("Update name:");
                    Console.WriteLine("Update ");
                }
                // Updatera bokning

            }

            catch
            {
                AnsiConsole.WriteLine("ID was not in the right format");
                Console.WriteLine("Press enter and try again");
                Console.ReadKey();
            }

        }
        public void DeleteGuest()
        {
            {
                ShowAllGuests();
                AnsiConsole.WriteLine("Enter ID of guest to delete");
                var guestToDelete = Convert.ToInt32(Console.ReadLine());
                _guestService.Delete(guestToDelete);
            }
        }
    }
}
