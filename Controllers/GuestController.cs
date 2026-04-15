using HotelApp1.Models;
using HotelApp1.Services;
using HotelApp1.UI.Prompts;
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
            var FirstName = GuestPrompts.PromptFirstname();
            var LastName = GuestPrompts.PromptLastname();
            var Street = GuestPrompts.PromptStreet();
            var PostalCode = GuestPrompts.PromptPostalcode();
            var City = GuestPrompts.PromptCity();
            var Country = GuestPrompts.PromptCountry();
            var Email = GuestPrompts.PromptEmail();
            var Phone = GuestPrompts.PromptPhone();
            var IsDeleted = false;
            var IsActive = true;

            var newGuest = new Guest
            {
                FirstName = FirstName,
                LastName = LastName,
                Street = Street,
                PostalCode = PostalCode,
                City = City,
                Country = Country,
                Email = Email,
                Phone = Phone,
                IsDeleted = IsDeleted,
                IsActive = IsActive,
            };
            _guestService.AddNewGuest(newGuest);
        }
        public void ShowAllGuests(List<Guest> guests)
        {
            var table = new Table();
            table.Border = TableBorder.Double;
            table.ShowRowSeparators = true;

            table.AddColumn("Guest ID");
            table.AddColumn("Name");
            table.AddColumn("Adress");
            table.AddColumn("Email");
            table.AddColumn("Phone number");
            table.AddColumn("Number of bookings");

            foreach (var guest in guests)
            {
                table.AddRow($"{guest.Id}", 
                    $"{guest.FirstName + " " + guest.LastName}", 
                    $"{guest.Street + ", " + guest.PostalCode + " " + guest.City + ", " +guest.Country}", 
                    $"{guest.Email}", 
                    $"{guest.Phone}", 
                    $"{guest.Bookings.Count}");
            }
            AnsiConsole.Write(table);
        }
        public void AddDeletedGuest()
        {
            GetAllDeletedGuests();
            AnsiConsole.WriteLine("Enter ID of guest to activate");
            try
            {
                var id = Convert.ToInt32(Console.ReadLine());
                var guestToAdd = GetById(id);
                if (guestToAdd == null)
                {
                    Console.WriteLine($"Guest with id {id} not found");
                    return;
                }
                if (guestToAdd.IsDeleted == true)
                {
                    _guestService.AddDeletedGuest(guestToAdd);
                    //guestToAdd.IsDeleted = false;
                    //guestToAdd.IsActive = true;
                    AnsiConsole.WriteLine($"{guestToAdd.FirstName + " " + guestToAdd.LastName} successfully activated!");
                    return;
                }
                if (guestToAdd.IsDeleted == false) 
                {
                    Console.WriteLine($"Guest with id {id} is already active");
                }
            }
            catch
            {
                AnsiConsole.WriteLine("Wrong ID");
            }
        }
        public void UpdateGuest()
        {
            GetAllActiveGuests();
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
                var existingGuest = _guestService.GetById(id);
                if (existingGuest == null)
                {
                    Console.WriteLine("Guest not found");
                    return;
                }
                else
                {
             
                    var newFirstName = GuestPrompts.PromptFirstname();
                    var newLastName = GuestPrompts.PromptLastname();
                    var newStreet = GuestPrompts.PromptStreet();
                    var newPostalCode = GuestPrompts.PromptPostalcode();
                    var newCity = GuestPrompts.PromptCity();
                    var newCountry = GuestPrompts.PromptCountry();
                    var newEmail = GuestPrompts.PromptEmail();
                    var newPhone = GuestPrompts.PromptPhone();
                    var newIsDeleted = false;
                    var newIsActive = true;

                    var updatedGuest = new Guest
                    {
                        FirstName = newFirstName,
                        LastName = newLastName,
                        Street = newStreet,
                        PostalCode = newPostalCode,
                        City = newCity,
                        Country = newCountry,
                        Email = newEmail,
                        Phone = newPhone,
                        IsDeleted = newIsDeleted,
                        IsActive = newIsActive,
                    };
                    _guestService.UpdateGuest(existingGuest, updatedGuest);
                }
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
                GetAllActiveGuests();
                AnsiConsole.WriteLine("Enter ID of guest to delete");
                var guestToDelete = Convert.ToInt32(Console.ReadLine());
                _guestService.Delete(guestToDelete);
            }
        }
        public Guest GetById(int id)
        {
            return _guestService.GetById(id);
        }

        public void GetAllGuests()
        {
            var allGuests = _guestService.GetAllGuests();
            AnsiConsole.MarkupLine("[plum1]--LIST OF ALL GUESTS--[/]");
            ShowAllGuests(allGuests);
        }

        public void GetAllActiveGuests()
        {
            var allActiveguests = _guestService.GetAllActiveGuests();
            AnsiConsole.MarkupLine("[plum1]--LIST OF ALL ACTIVE GUESTS--[/]");
            ShowAllGuests(allActiveguests);
        }

        public void GetAllGuestsToday()
        {
            var guestsToday = _guestService.GetAllGuestsToday(); 
            AnsiConsole.MarkupLine("[plum1]--LIST OF ALL GUESTS CHECKED-IN TODAY--[/]");
            ShowAllGuests(guestsToday);
        }

        public void GetAllDeletedGuests()
        {
            var deletedGuests = _guestService.GetAllDeletedGuests();
            AnsiConsole.MarkupLine("[plum1]--LIST OF ALL DELETED GUESTS--[/]");
            ShowAllGuests(deletedGuests);
        }
    }
}
