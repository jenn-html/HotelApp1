using System.Globalization;

namespace HotelApp1.UI
{
    public static class ConsoleCalendar
    {
        public static DateTime PickDate(string title)
        {
            var month = DateTime.Today;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(title);
                RenderMonth(month);

                Console.WriteLine();
                Console.WriteLine("Enter day number to pick date, 'n' = next month, 'p' = prev month, 'q' = cancel");
                Console.Write("> ");
                var input = Console.ReadLine()?.Trim().ToLowerInvariant();

                if (input == "n") { month = month.AddMonths(1); continue; }
                if (input == "p") { month = month.AddMonths(-1); continue; }
                if (input == "q") throw new OperationCanceledException("Date selection cancelled.");

                if (int.TryParse(input, out var day))
                {
                    if (day >= 1 && day <= DateTime.DaysInMonth(month.Year, month.Month))
                    {
                        return new DateTime(month.Year, month.Month, day);
                    }
                }

                Console.WriteLine("Invalid input. Press any key to continue...");
                Console.ReadKey(true);
            }
        }

        private static void RenderMonth(DateTime month)
        {
            var ci = CultureInfo.CurrentCulture;
            var first = new DateTime(month.Year, month.Month, 1);
            var days = DateTime.DaysInMonth(month.Year, month.Month);
            var weekdayHeader = string.Join(" ", ci.DateTimeFormat.AbbreviatedDayNames);

            Console.WriteLine();
            Console.WriteLine($"{month:MMMM yyyy}".PadLeft(15));
            Console.WriteLine(weekdayHeader);

            var indent = ((int)first.DayOfWeek + 7 - (int)ci.DateTimeFormat.FirstDayOfWeek) % 7;
            for (int i = 0; i < indent; i++) Console.Write("    ");

            for (int d = 1; d <= days; d++)
            {
                var date = new DateTime(month.Year, month.Month, d);
                var text = d.ToString().PadLeft(2);
                if (date == DateTime.Today) text = "[" + text + "]";
                else text = " " + text + " ";
                Console.Write(text + " ");
                indent++;
                if (indent % 7 == 0) Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}

