
using HotelApp1.Controllers;
using HotelApp1.Models;
using Spectre.Console;
using System.Text;

namespace HotelApp1.UI
{
    public class BookingAvailabilityCalendar
    {
        public readonly BookingController _bookingController;
        public BookingAvailabilityCalendar(BookingController bookingController)
        {
            _bookingController = bookingController;
        }
        public void ShowAvailability(int id)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var today = DateTime.Today;
            var checkIn = PickDate("Select check-in date", new DateTime(today.Year, today.Month, 1));
            if (!checkIn.HasValue) return;
            if (checkIn < today)
            {
                AnsiConsole.MarkupLine("[red]Check-in date can NOT be in the past[/]");
                return;
            }

            var checkOutStart = checkIn.Value.AddDays(1);
            var checkOut = PickDate("Select check-out date", checkOutStart);
            if (!checkOut.HasValue) return;

            if (checkOut <= checkIn)
            {
                AnsiConsole.MarkupLine("[red]Check-out date has to be after check-in date.[/]");
                //Console.ReadKey(true);
                return;
            }

            var guests = AnsiConsole.Prompt(
                new TextPrompt<int>("[yellow]Select number of guests:[/]")
                    .Validate(g => g > 0 && g < 5 ? ValidationResult.Success() : ValidationResult.Error("[red]Minimum 1 guest and maximum 4 per room[/]"))
            );

            var selectedCheckinDate = Convert.ToDateTime(checkIn);
            var selectedCheckoutDate = Convert.ToDateTime(checkOut);
            List<Room> available = new List<Room>();
            if (id == 0)
            {
                available = _bookingController.GetAvailibleRooms(selectedCheckinDate, selectedCheckoutDate, guests);
            }
            else
            {
                available = _bookingController.GetAvailableRoomsToUpdateBooking(selectedCheckinDate,selectedCheckoutDate, guests,id);
            }
            var selectedRoom = 0;
            bool continueLoop = true;
            while (continueLoop == true)
            {
                Console.Clear();
                AnsiConsole.MarkupLine($"[green1]Availible rooms from {checkIn:yyyy-MM-dd} to {checkOut:yyyy-MM-dd} for {guests} guests:[/]");
                if (!available.Any())
                {
                    AnsiConsole.MarkupLine("[red]No availible rooms for the choosen date and number of guests.[/]");
                    break;
                }
                else
                {
                    var table = new Table().HeavyHeadBorder().AddColumn("Room #").AddColumn("Room type").AddColumn("Size(kvm)").AddColumn("Capacity(guests)").AddColumn("Extra beds availible");
                    foreach (var r in available)
                    {

                        var capacity = r.MaxGuestsPerRoomWithoutExtrabeds + r.ExtraBedsAvailable;
                        table.AddRow(r.RoomNumber.ToString(), r.RoomType.Type.ToString(), r.RoomSize.ToString("0.##"), capacity.ToString(), r.ExtraBedsAvailable.ToString());
                    }
                    AnsiConsole.Write(table);
                }

                try
                {
                    AnsiConsole.MarkupLine("Enter Roomnumber of choice to continue with booking");
                    selectedRoom = Convert.ToInt32(Console.ReadLine());
                    foreach (var r in available)
                    {
                        if (r.RoomNumber == selectedRoom)
                        {
                            selectedRoom = r.Id;
                            continueLoop = false;
                            break;
                        }
                    }
                }
                catch
                {
                    AnsiConsole.WriteLine("Selected room number invalid");
                    AnsiConsole.WriteLine("Press enter to try again");
                    Console.ReadKey();
                }
            }
            if (id == 0)
            {
                _bookingController.AddNewBooking(selectedCheckinDate, selectedCheckoutDate, guests, selectedRoom);
            }
            else 
            {
                _bookingController.UpdateBooking(selectedCheckinDate, selectedCheckoutDate, guests, selectedRoom, id);
            }
        }

        private static DateTime? PickDate(string title, DateTime initial)
        {
            var selectedDate = initial;

            while (true)
            {
                Console.Clear();
                RenderCalendar(title, selectedDate);

                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.RightArrow:
                        selectedDate = selectedDate.AddDays(1);
                        break;
                    case ConsoleKey.LeftArrow:
                        selectedDate = selectedDate.AddDays(-1);
                        break;
                    case ConsoleKey.UpArrow:
                        selectedDate = selectedDate.AddDays(-7);
                        break;
                    case ConsoleKey.DownArrow:
                        selectedDate = selectedDate.AddDays(7);
                        break;
                    case ConsoleKey.PageUp:
                        selectedDate = selectedDate.AddMonths(-1);
                        break;
                    case ConsoleKey.PageDown:
                        selectedDate = selectedDate.AddMonths(1);
                        break;
                    case ConsoleKey.Enter:
                        AnsiConsole.MarkupLine($"\n[green1]Valt: {selectedDate:yyyy-MM-dd}[/]");
                        return selectedDate;
                    case ConsoleKey.Escape:
                        return null;
                }
            }
        }

        private static void RenderCalendar(string title, DateTime selectedDate)
        {
            var stringWriter = new StringWriter();

            stringWriter.WriteLine($"[Green1]{title}[/]\n");
            stringWriter.WriteLine($"[DeepPink2]{selectedDate:MMMM}[/]".ToUpper());
            stringWriter.WriteLine("Mån  Tis  Ons  Tor  Fre  Lör  Sön");
            stringWriter.WriteLine("─────────────────────────────────");

            DateTime firstDayOfMonth = new DateTime(selectedDate.Year, selectedDate.Month, 1);
            int daysInMonth = DateTime.DaysInMonth(selectedDate.Year, selectedDate.Month);
            int startDay = (int)firstDayOfMonth.DayOfWeek;
            startDay = (startDay == 0) ? 6 : startDay - 1; // Monday first

            for (int i = 0; i < startDay; i++)
            {
                stringWriter.Write("     ");
            }

            for (int day = 1; day <= daysInMonth; day++)
            {
                var date = new DateTime(selectedDate.Year, selectedDate.Month, day);
                if (date == selectedDate.Date)
                {
                    stringWriter.Write($"[Green1]{day,2}[/]   ");
                }
                else if (date == DateTime.Today)
                {
                    stringWriter.Write($"[DodgerBlue3]{day,2}[/]   ");
                }
                else
                {
                    stringWriter.Write($"{day,2}   ");
                }

                if ((startDay + day) % 7 == 0)
                {
                    stringWriter.WriteLine();
                }
            }

            var panel = new Panel(stringWriter.ToString())
            {
                Border = BoxBorder.Heavy,
                Header = new PanelHeader($"[red]{selectedDate:yyyy}[/]", Justify.Center)
            };

            AnsiConsole.Write(panel);
            AnsiConsole.MarkupLine("\n[white]Arrows for day, PageUp/PageDown for month, [Gold1]Enter[/] pick date, [yellow]Esc[/] exit[/]");
        }
    }
}
