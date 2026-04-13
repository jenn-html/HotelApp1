using HotelApp1.Controllers;
using HotelApp1.Data;
using HotelApp1.Repositories;
using HotelApp1.Services;
using HotelApp1.Utilities;
using HotelApp1.Utilities.Menus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
namespace HotelApp1
{
    public class Application()
    {
        public void Run()
        {
            ConsoleSetup.EnableUtf8AndAnsi();
            var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true);
            var config = builder.Build();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = config.GetConnectionString("DefaultConnection");
            var context = new ApplicationDbContext();
            
            options.UseSqlServer(connectionString);

            using (var dbContext = new ApplicationDbContext(options.Options))
            {
                dbContext.Database.Migrate();
                if (!(dbContext.Guests.Any() || dbContext.Rooms.Any() || dbContext.Bookings.Any() || dbContext.RoomTypes.Any())) // choose a table that should exist after seeding
                {
                    DataInitializer.Initialize(dbContext);
                }
            }
      
            var guestRepository = new GuestRepository(context);
            var guestService = new GuestService(guestRepository);
            var guestController = new GuestController(guestService);
            var bookingRepository = new BookingRepository(context);
            var bookingService = new BookingService(bookingRepository);
            var bookingController = new BookingController(bookingService, guestService);
            Console.WriteLine(": \"\r\n _____                                                                                       _____ \r\n( ___ )                                                                                     ( ___ )\r\n |   |~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|   | \r\n |   |                                                                                       |   | \r\n |   |                                                                                       |   | \r\n |   | ██╗  ██╗ ██████╗ ████████╗███████╗██╗              ██╗███████╗███╗   ██╗███╗   ██╗██╗ |   | \r\n |   | ██║  ██║██╔═══██╗╚══██╔══╝██╔════╝██║              ██║██╔════╝████╗  ██║████╗  ██║██║ |   | \r\n |   | ███████║██║   ██║   ██║   █████╗  ██║              ██║█████╗  ██╔██╗ ██║██╔██╗ ██║██║ |   | \r\n |   | ██╔══██║██║   ██║   ██║   ██╔══╝  ██║         ██   ██║██╔══╝  ██║╚██╗██║██║╚██╗██║██║ |   | \r\n |   | ██║  ██║╚██████╔╝   ██║   ███████╗███████╗    ╚█████╔╝███████╗██║ ╚████║██║ ╚████║██║ |   | \r\n |   | ╚═╝  ╚═╝ ╚═════╝    ╚═╝   ╚══════╝╚══════╝     ╚════╝ ╚══════╝╚═╝  ╚═══╝╚═╝  ╚═══╝╚═╝ |   | \r\n |   |                                                                                       |   | \r\n |   |                                                                                       |   | \r\n |___|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|___| \r\n(_____)                                                                                     (_____)\r\n\"");
            var mainmenu = new MainMenu(bookingController, guestController, context);
            mainmenu.ShowMenu();
        }
    }
}
