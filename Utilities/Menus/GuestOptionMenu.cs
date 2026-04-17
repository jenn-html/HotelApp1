using HotelApp1.Controllers;
using HotelApp1.UI;
using HotelApp1.Utilities.Interfaces;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Utilities.Menus
{
    public class GuestOptionMenu : IMenu
    {
        private readonly GuestController _guestController;
        public GuestOptionMenu(GuestController guestController)
        {
            _guestController = guestController;
        }
        public void ShowMenu()
        {
            var guestUpdateMenu = new GuestUpdateMenu(_guestController);
            var guestViewMenu = new GuestViewMenu(_guestController);
            string[] options = new[] {
                $"[orchid]VIEW GUESTS[/]",
                $"[orchid]ADD A NEW GUEST[/]",
                $"[orchid]UPDATE A GUEST[/]",
                $"[orchid]DELETE A GUEST[/]",
                $"[yellow]GO BACK TO MAIN[/]"
            };
            var displayMenu = true;
            while (displayMenu = true)
            {
                Console.Clear();

                Banner.HotelJenniBanner();
                // Use Spectre.Console SelectionPrompt for keyboard navigation + Enter selection
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[Orchid]---GUEST MENU---[/]")
                        .PageSize(5)
                        .AddChoices(options)
                        .WrapAround()
                );

                int selected = Array.IndexOf(options, choice);

                Console.Clear();
                switch (selected)
                {
                    case 0:
                        guestViewMenu.ShowMenu();
                        break;
                    case 1:
                        _guestController.AddNewGuest();
                        break;
                    case 2:
                        guestUpdateMenu.ShowMenu();
                        break;
                    case 3:
                        _guestController.DeleteGuest();
                        break;
                    case 4:
                        return;
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
