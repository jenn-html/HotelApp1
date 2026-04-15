using HotelApp1.Controllers;
using Spectre.Console;

namespace HotelApp1.Utilities.Menus
{
    public class GuestViewMenu : IMenu
    {
        private readonly GuestController _guestController;

        public GuestViewMenu(GuestController guestController)
        {
            _guestController = guestController;
        }

        public void ShowMenu()
        {

            string[] options = new[] {

                "All guests",
                "Active guests",
                "Checked-in guests today",
                "Deleted guests",
                "Go back to guest menu"
            };

            while (true)
            {
                Console.Clear();


                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[SeaGreen1]---VIEW GUEST MENU---[/]")
                        .PageSize(5)
                        .AddChoices(options)
                        .WrapAround()
                );

                int selected = Array.IndexOf(options, choice);

                Console.Clear();
                switch (selected)
                {

                    case 0:
                        _guestController.GetAllGuests();
                        break;
                    case 1:
                        _guestController.GetAllActiveGuests();
                        break;
                    case 2:
                        _guestController.GetAllGuestsToday();
                        break;
                    case 3:
                        _guestController.GetAllDeletedGuests();
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

