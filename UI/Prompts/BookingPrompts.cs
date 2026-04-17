using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.UI.Prompts
{
    public class BookingPrompts
    {
        public static int PromptEnterRoomNumber()
        {
            var roomnumber = AnsiConsole.Prompt(
                new TextPrompt<int>("[yellow]Select roomnumber:[/]")
                .Validate(g => g > 199 ? ValidationResult.Success() : ValidationResult.Error("[red]All roomnumbers start from 200, try again[/]")));
            return roomnumber;
        }
        public static int PromptEnterId()
        {
            var Id = AnsiConsole.Prompt(
            new TextPrompt<int>("[yellow]Select ID:[/]")
             .Validate(g => g > 0 ? ValidationResult.Success() : ValidationResult.Error("[red]Enter a value over 0[/]")));
             return Id;
        }

    }
}
