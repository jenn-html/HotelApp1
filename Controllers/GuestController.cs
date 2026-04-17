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
            AnsiConsole.MarkupLine("[plum1]--ADD NEW GUEST--[/]");
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
                IsActive = IsActive,
            };
            _guestService.Add(newGuest);
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
               var id = BookingPrompts.PromptEnterId();
                var guestToAdd = GetById(id);
                if (guestToAdd == null)
                {
                    Console.WriteLine($"Guest with id {id} not found");
                    return;
                }
                if (guestToAdd.IsActive == false)
                {
                    _guestService.AddDeletedGuest(guestToAdd);
                    AnsiConsole.WriteLine($"{guestToAdd.FirstName + " " + guestToAdd.LastName} successfully activated!");
                    return;
                }
                if (guestToAdd.IsActive == true) 
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
            AnsiConsole.WriteLine("Enter ID of Guest to update");
            try
            {
                var inputId = BookingPrompts.PromptEnterId();
               
                var id = Convert.ToInt32(inputId);
                var existingGuest = _guestService.GetById(id);
                if (existingGuest == null)
                {
                    AnsiConsole.MarkupLine($"[red]Guest not found[/]");
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
                        IsActive = newIsActive,
                    };
                    _guestService.Update(existingGuest, updatedGuest);
                    AnsiConsole.MarkupLine($"[green]Guest updated![/]");
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
                try
                {
                    var guestToDelete = BookingPrompts.PromptEnterId();
                    var existingGuest = GetById(guestToDelete);
                    if (existingGuest == null)
                    {
                        Console.WriteLine($"Can't find guest with id :{guestToDelete}");
                        return;
                    }

                    var hasActiveBooking = _guestService.GetActiveBookingsByGuestId(guestToDelete);
                    if (hasActiveBooking.Any())
                    {
                        Console.WriteLine("You can only delete a guest that don't have an active booking.");
                        return;
                    }

                    _guestService.DeleteGuest(existingGuest);
                    AnsiConsole.MarkupLine($"[green]Guest deleted![/]");
                }
                catch
                {
                    AnsiConsole.MarkupLine($"[red]Not able to delete guest[/]");
                }
            }
        }
        public Guest GetById(int id)
        {
            return _guestService.GetById(id);
        }

        public void GetAllGuests()
        {
            var allGuests = _guestService.GetAll();
            AnsiConsole.MarkupLine("[plum1]--LIST OF ALL GUESTS--[/]");
            if (allGuests.Any())
            {
                ShowAllGuests(allGuests);
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]No guests found[/]");
                return;
            }
        }

        public void GetAllActiveGuests()
        {
            var allActiveguests = _guestService.GetAllActiveGuests();
            AnsiConsole.MarkupLine("[plum1]--LIST OF ALL ACTIVE GUESTS--[/]");
            if (allActiveguests.Any())
            {            
                ShowAllGuests(allActiveguests);
            }
            else 
            {
                AnsiConsole.MarkupLine($"[red]No guests found[/]");
                return;
            }

        }

        public void GetAllGuestsToday()
        {
            var guestsToday = _guestService.GetAllGuestsToday(); 
            AnsiConsole.MarkupLine("[plum1]--LIST OF ALL GUESTS CHECKED-IN TODAY--[/]");
            if (guestsToday.Any())
            {ShowAllGuests(guestsToday);
                
            }
            else
            {
            AnsiConsole.MarkupLine($"[red]No guests found[/]");
                return;
            }
        }

        public void GetAllDeletedGuests()
        {
            var deletedGuests = _guestService.GetAllDeletedGuests();
            AnsiConsole.MarkupLine("[plum1]--LIST OF ALL DELETED GUESTS--[/]");
            if (deletedGuests.Any())
            {
                ShowAllGuests(deletedGuests);

            }
            else
            {
                AnsiConsole.MarkupLine($"[red]No guests found[/]");
                return;
            }
        }
    }
}
