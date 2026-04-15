using HotelApp1.Controllers;
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

                "Uppdate guest",
                "Activete deleted guest",
                "Go back to guest menu"
            };

            while (true)
            {
                Console.Clear();


                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[Plum1]---UPDATE GUEST MENU---[/]")
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
                        Console.WriteLine("Invalid option.");
                        break;
                }
                Console.WriteLine("Press any key to go back to the menu...");
                Console.ReadKey();
            }
        }
    }
}
