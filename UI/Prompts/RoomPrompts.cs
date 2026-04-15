using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.UI.Prompts
{
    public class RoomPrompts
    {
        public static int PromptRoomSize()
        {
            var roomSize = AnsiConsole.Prompt(
              new TextPrompt<int>("[yellow]Room size:[/]")
                  .Validate(g => g < 50 && g > 10 ? ValidationResult.Success() : ValidationResult.Error("[red]Enter a roomnumber [/]"))
          );
            return roomSize;
        }
        public static int PromptRoomType()
        {
            var roomType = AnsiConsole.Prompt(
               new TextPrompt<int>("[yellow]Room type:[/]")
                   .Validate(g => g >= 0 ? ValidationResult.Success() : ValidationResult.Error("[red]You must enter a value[/]"))
           );
            return roomType;
        }
        public static string PromptRoomMaxGuests()
        {
            var roomMaxguests = AnsiConsole.Prompt(
               new TextPrompt<string>("[yellow]Room capacity without extra beds:[/]")
                   .Validate(g => g.Length <= 4 && g.Length >= 1 ? ValidationResult.Success() : ValidationResult.Error("[red]Minimum 1 and maximum 4[/]"))
           );
            return roomMaxguests;
        }
        
    }
}
