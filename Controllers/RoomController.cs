using HotelApp1.Models;
using HotelApp1.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Controllers
{
    public class RoomController
    {
        private readonly RoomService _roomService;

        public RoomController(RoomService roomService)
        {
            _roomService = roomService;
        }
        public void ShowAllRooms()
        {
            AnsiConsole.MarkupLine("[Orange1]--LIST OF ALL ROOMS[/]");
            var rooms = _roomService.GetAllRooms();
            var table = new Table();
            table.Border = TableBorder.Double;
            table.ShowRowSeparators = true;

            table.AddColumn("Room ID");
            table.AddColumn("Room number");
            table.AddColumn("Type");
            table.AddColumn("Size(kvm)");
            table.AddColumn("Capacity without extrabeds");
            table.AddColumn("Available extrabeds");
            table.AddColumn("Total capacity");
            table.AddColumn("Price per night");
            table.AddColumn("Price per extrabed");


            foreach (var room in rooms)
            {
                table.AddRow($"{room.Id}",
                    $"{room.RoomNumber}",
                    $"{room.RoomType.Type}",
                    $"{room.RoomSize}",
                    $"{room.MaxGuestsPerRoomWithoutExtrabeds}",
                    $"{room.ExtraBedsAvailable}",
                    $"{room.MaxGuestsPerRoomWithoutExtrabeds + room.ExtraBedsAvailable}",
                    $"{room.PricePerNight}",
                    $"{room.PriceExtraBeds}");
            }
            AnsiConsole.Write(table);
        
        }

        public Room GetById(int roomId)
        {

            return _roomService.GetById(roomId);
        }

        public void DeleteRoom()
        {
            throw new NotImplementedException();
        }

        public void UpdateRoom()
        {
            throw new NotImplementedException();
        }

        public void AddNewRoom()
        {
            throw new NotImplementedException();
        }
    }

}
