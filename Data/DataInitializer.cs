using HotelApp1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Data
{
    public class DataInitializer
    {
        public static void Initialize(ApplicationDbContext dbContext)
        {
            var roomType1 = new RoomType { Type = "Single room" };
            var roomType2 = new RoomType { Type = "Double room" };
            var room1 = new Room { RoomSize = 10, RoomType = roomType1, ExtraBedsAvailable = 0, MaxGuestsPerRoomWithoutExtrabeds = 1 , PricePerNight = 1200};
            var room2 = new Room { RoomSize = 10, RoomType = roomType1, ExtraBedsAvailable = 0, MaxGuestsPerRoomWithoutExtrabeds = 1 , PricePerNight = 1200};
            var room3 = new Room { RoomSize = 20, RoomType = roomType2, ExtraBedsAvailable = 1, MaxGuestsPerRoomWithoutExtrabeds = 2 , PricePerNight = 2400};
            var room4 = new Room { RoomSize = 20, RoomType = roomType2, ExtraBedsAvailable = 1, MaxGuestsPerRoomWithoutExtrabeds = 2 , PricePerNight = 2400};
            var room5 = new Room { RoomSize = 35, RoomType = roomType2, ExtraBedsAvailable = 2, MaxGuestsPerRoomWithoutExtrabeds = 2 , PricePerNight = 4200};
            var room6 = new Room { RoomSize = 35, RoomType = roomType2, ExtraBedsAvailable = 2, MaxGuestsPerRoomWithoutExtrabeds = 2 , PricePerNight = 4200};
            var room7 = new Room { RoomSize = 40, RoomType = roomType2, ExtraBedsAvailable = 2, MaxGuestsPerRoomWithoutExtrabeds = 2 , PricePerNight = 4800};
            var room8 = new Room { RoomSize = 12.5, RoomType = roomType1, ExtraBedsAvailable = 0, MaxGuestsPerRoomWithoutExtrabeds = 1 ,PricePerNight = 1500};
            var room9 = new Room { RoomSize = 12.5, RoomType = roomType1, ExtraBedsAvailable = 0, MaxGuestsPerRoomWithoutExtrabeds = 1 , PricePerNight = 1500};

            var guest1 = new Guest { FirstName = "Alfons", LastName = "Åberg", Email = "jag_ska_bara@nu-snart.se", Street = "Höghuset vid Parken v3", PostalCode = "12345", City = "Göteborg", Country = "Sweden" , Phone = "070-0000000"};
            var guest2 = new Guest { FirstName = "Buzz", LastName = "Lightyear", Email = "to_infinity_and_beyond@starcommand.gov", Street = "Gamma-kvadranten, Sektor 4", PostalCode = "01-0000", City = "Stjärnbas 1", Country = "Rymden" };
            var guest3 = new Guest { FirstName = "Captain", LastName = "Hook", Email = "tick_tock@neverland.sea", Street = "Jolly Roger", PostalCode = "00-01-02", City = "Krokodilbukten", Country = "Neverland" };
            var guest4 = new Guest { FirstName = "Donald", LastName = "Duck", Email = "no_luck_duck@disaster.net", Street = "Paradisäppelvägen 111", PostalCode = "1313", City = "Ankeborg", Country = "USA" , Phone = "1313-1313"};
            var guest5 = new Guest { FirstName = "Elvis", LastName = "Presley", Email = "king_of_rock_and_roll@parties.com", Street = "Graceland", PostalCode = "TN 38116", City = "Memphis", Country = "USA" };
            var guest6 = new Guest { FirstName = "Frodo", LastName = "Baggins", Email = "the_brave_hobbit@no-ring-no-problem.now", Street = "Under-Hill", PostalCode = "SH 01", City = "Hobbiton", Country = "Midgård" };
            var guest7 = new Guest { FirstName = "Gomez", LastName = "Addams", Email = "cara_mia@creepy.fr", Street = "Cemetery Lane 01", PostalCode = "666 13", City = "Westfield", Country = "USA" };
            var guest8 = new Guest { FirstName = "Harry", LastName = "Potter", Email = "im_a_wizard@hogwarts.now", Street = "Priet Drive 4", PostalCode = "WH1 2PQ", City = "Little Whinging, Surrey", Country = "UK" };
            var guest9 = new Guest { FirstName = "Indiana", LastName = "Jones", Email = "no_map_needed@lost.com", Street = "LostRoad 13", PostalCode = "06511", City = "Fairfield", Country = "USA" };
       
            dbContext.RoomTypes.AddRange(new List<RoomType>
            { 
                roomType1, roomType2
            });
            dbContext.SaveChanges();
            dbContext.Rooms.AddRange(new List<Room>
            {
                room1, room2, room3, room4, room5, room6, room7, room8, room9
            });
            dbContext.SaveChanges();
            dbContext.Guests.AddRange(new List<Guest>
            {
                guest1, guest2, guest3, guest4, guest5, guest6, guest7, guest8, guest9
            });
            dbContext.SaveChanges();
            dbContext.Bookings.AddRange(new List<Booking> 
            {
               new Booking { Guest = guest1, Room = room3, NumberOfGuests = 2, CheckIn = DateTime.Today.AddDays(-1), CheckOut = DateTime.Today.AddDays(2)},
              new Booking { Guest = guest8, Room = room7, NumberOfGuests = 3, CheckIn = DateTime.Today.AddDays(2), CheckOut = DateTime.Today.AddDays(3)},
              new Booking { Guest = guest9, Room = room3, NumberOfGuests = 2, CheckIn = DateTime.Today.AddDays(3), CheckOut = DateTime.Today.AddDays(4)},
              new Booking { Guest = guest6, Room = room5, NumberOfGuests = 4, CheckIn = DateTime.Today.AddDays(7), CheckOut = DateTime.Today.AddDays(15)},
              new Booking { Guest = guest7, Room = room6, NumberOfGuests = 4, CheckIn = DateTime.Today.AddDays(13), CheckOut = DateTime.Today.AddDays(26)},
              new Booking { Guest = guest4, Room = room2, NumberOfGuests = 1, CheckIn = DateTime.Today.AddDays(100), CheckOut = DateTime.Today.AddDays(109)}
            });
            dbContext.SaveChanges();
        }
    }
}
