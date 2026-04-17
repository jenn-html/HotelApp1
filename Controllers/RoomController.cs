using HotelApp1.Models;
using HotelApp1.Services;
using HotelApp1.UI.Prompts;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.EntityFrameworkCore;
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
        public void GetAllRooms()
        {  
            AnsiConsole.MarkupLine("[MediumPurple3_1]--LIST OF ALL ROOMS[/]");
            var rooms = _roomService.GetAllRooms();
            if (rooms == null)
            {
                AnsiConsole.MarkupLine($"[red]No rooms here[/]");
                return;
            }
            ShowAllRooms(rooms);
        }

        public void ShowAllRooms(List<Room> rooms)
        {
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

        public Room GetRoomById(int roomId)
        {

            return _roomService.GetById(roomId);
        }
        public RoomType GetRoomtypeById(int id)
        {
            return _roomService.GetRoomtypeById(id);
        }

        public void DeleteRoom(BookingController bookingController)
        {
            bool run = true;
            while (run)
            {
                AnsiConsole.MarkupLine($"[MediumPurple3_1]---DELETE ROOM---[/]");
                GetAllRooms();
                var id = RoomPrompts.PromptEnterId();
                var room = GetRoomById(id);
                if (room == null)
                {
                    AnsiConsole.MarkupLine($"[red]Room with id {id} not found[/]");
                    if (AnsiConsole.Confirm("Try agan?"))
                    {
                        Console.Clear();
                    }
                    else
                    {
                        return;
                    }
                }
                else 
                {
                    var hasBookings = _roomService.GetRoomThatHasBookings(room);
                    if ((hasBookings.Any()))
                    {
                        AnsiConsole.MarkupLine($"[red]Can't delete a room that have bookings![/]");
                        AnsiConsole.MarkupLine($"Current bookings for room with id {room.Id}:");
                        bookingController.ShowBookings(hasBookings);
                    }
                    else
                    {
                        _roomService.DeleteRoom(room); 
                        AnsiConsole.MarkupLine($"[green]Room deleted![/]"); 
                        run = false;
                     break;
                    }
                }
            }
        }

        public void UpdateRoom(BookingController bookingController)
        {
            bool run = true;
            while (run)
            {
                AnsiConsole.MarkupLine($"[MediumPurple3_1]--UPDATE ROOM--[/]");
                GetAllRooms();
                var id = RoomPrompts.PromptEnterId();
                var roomToUpdate = GetRoomById(id);
                
                if (roomToUpdate == null)
                {
                    AnsiConsole.MarkupLine($"[red]Room with id {id} not found[/]");
                    if (AnsiConsole.Confirm("Try agan?"))
                    {
                        Console.Clear();
                    }
                    else
                    {
                        run = false;
                        return;
                    }
                }
                else
                {
                    var hasBookings = _roomService.GetRoomThatHasBookings(roomToUpdate);
                    if ((hasBookings.Any()))
                    {
                        Console.Clear();
                        AnsiConsole.MarkupLine($"[red]Can't update a room that have bookings![/]");
                        AnsiConsole.MarkupLine($"Current bookings for room with id {roomToUpdate.Id}:");
                        bookingController.ShowBookings(hasBookings);
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                    else
                    {
                        var roomSize = RoomPrompts.PromptRoomSize();
                        var roomType = RoomPrompts.PromptRoomType();
                        var type = GetRoomtypeById(roomType);
                        var pricePerSquarefeet = RoomPrompts.PromptPriceSquareFeet();
                        decimal priceExtrabeds = 200;
                        decimal totalprice = pricePerSquarefeet * roomSize;
                        var capacityWithoutExtraBeds = roomType;
                        var numberofextrabeds = 0;
                        if (roomSize >= 20 && roomType == 2)
                        {
                            numberofextrabeds = 1;
                        }
                        if (roomSize >= 35 && roomType == 2)
                        {
                            numberofextrabeds = 2;
                        }

                        var newRoom = new Room
                        {
                            RoomSize = roomSize,
                            RoomType = type,
                            PricePerSquarefeet = pricePerSquarefeet,
                            MaxGuestsPerRoomWithoutExtrabeds = capacityWithoutExtraBeds,
                            ExtraBedsAvailable = numberofextrabeds,
                            ExtraBedsAdded = 0,
                            PriceExtraBeds = priceExtrabeds,
                            PricePerNight = totalprice,
                            IsActive = true
                        };
                        _roomService.Update(newRoom, roomToUpdate);
                        run = false;
                        return;
                    }
                }
            }
           
        }

        public void AddNewRoom()
        {
            AnsiConsole.MarkupLine($"[MediumPurple3_1]--ADD NEW ROOM--[/]");
            var roomSize = RoomPrompts.PromptRoomSize();
            var roomType = RoomPrompts.PromptRoomType();
            var type = GetRoomtypeById(roomType);
            var pricePerSquarefeet = RoomPrompts.PromptPriceSquareFeet();
            decimal priceExtrabeds = 200;
            decimal totalprice = pricePerSquarefeet * roomSize;
            var capacityWithoutExtraBeds = roomType;
            var numberofextrabeds = 0;
            if (roomSize >= 20 && roomType == 2)
            {
                numberofextrabeds = 1;
            }
            if (roomSize >= 35 && roomType == 2)
            {
                numberofextrabeds = 2;
            }

            var newRoom = new Room
            {
                RoomSize = roomSize,
                RoomType = type,
                PricePerSquarefeet = pricePerSquarefeet,
                MaxGuestsPerRoomWithoutExtrabeds = capacityWithoutExtraBeds,
                ExtraBedsAvailable = numberofextrabeds,
                ExtraBedsAdded = 0,
                PriceExtraBeds = priceExtrabeds,
                PricePerNight = totalprice,
                IsActive = true
            };
            _roomService.AddNewRoom(newRoom);
            AnsiConsole.MarkupLine($"[green]New room added![/]");
        }
    }

}
