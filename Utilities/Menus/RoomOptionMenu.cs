using HotelApp1.Controllers;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Utilities.Menus
{
    public class RoomOptionMenu 
    {
        private readonly RoomController _roomController;
        public RoomOptionMenu(RoomController roomController)
        {
            _roomController = roomController;
        }
        public void ShowMenu(BookingController bookingController)
        {

            string[] options = new[] {
                $"[MediumPurple3_1]SHOW ALL ROOMS[/]",
                $"[MediumPurple3_1]ADD A NEW ROOM[/]",
                $"[MediumPurple3_1]UPDATE A ROOM[/]",
                $"[MediumPurple3_1]DELETE A ROOM[/]",
                $"[yellow]GO BACK TO MAIN[/]"
            };

            while (true)
            {
                Console.Clear();

                // Use Spectre.Console SelectionPrompt for keyboard navigation + Enter selection
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[MediumPurple3_1]---ROOM MENU---[/]")
                        .PageSize(5)
                        .AddChoices(options)
                        .WrapAround()
                );

                int selected = Array.IndexOf(options, choice);

                Console.Clear();
                switch (selected)
                {
                    case 0:
                        _roomController.GetAllRooms();
                        break;
                    case 1:
                        _roomController.AddNewRoom();
                        break;
                    case 2:
                        _roomController.UpdateRoom(bookingController);
                        break;
                    case 3:
                        _roomController.DeleteRoom(bookingController);
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
