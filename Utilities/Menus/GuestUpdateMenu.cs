using HotelApp1.Controllers;
using HotelApp1.UI;
using HotelApp1.Utilities.Interfaces;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Utilities.Menus
{
    public class GuestUpdateMenu : IMenu
    {
        private readonly GuestController _guestController;

        public GuestUpdateMenu(GuestController guestController)
        {
            _guestController = guestController;
        }

        public void ShowMenu()
        {

            string[] options = new[] {

                $"[orchid]UPDATE GUEST[/]",
                $"[orchid]REACTIVATE DELETED GUEST[/]",
                $"[yellow]GO BACK TO GUEST MENU[/]"
            };

            while (true)
            {
                Console.Clear();

                Banner.HotelJenniBanner();

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[orchid]---UPDATE GUEST MENU---[/]")
                        .PageSize(3)
                        .AddChoices(options)
                        .WrapAround()
                );

                int selected = Array.IndexOf(options, choice);

                Console.Clear();
                switch (selected)
                {

                    case 0:
                        _guestController.UpdateGuest();
                        break;
                    case 1:
                        _guestController.AddDeletedGuest();
                        break;
                    case 2:
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
