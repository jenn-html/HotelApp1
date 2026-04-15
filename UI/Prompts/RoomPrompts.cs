using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.UI.Prompts
{
    public class RoomPrompts
    {
        public static int PromptRoomNumber()
        {
            var roomType = AnsiConsole.Prompt(
              new TextPrompt<int>("[yellow]Room type:[/]")
                  .Validate(g => g > 200 ? ValidationResult.Success() : ValidationResult.Error("[red]Enter a roomnumber [/]"))
          );
            return roomType;
        }
        public static string PromptRoomName()
        {
            var roomType = AnsiConsole.Prompt(
               new TextPrompt<string>("[yellow]Room type:[/]")
                   .Validate(g => g.Length <= 50 && g.Length > 1 ? ValidationResult.Success() : ValidationResult.Error("[red]Minimum 2 character and maximum 50[/]"))
           );
            return roomType;
        }
    }
}
