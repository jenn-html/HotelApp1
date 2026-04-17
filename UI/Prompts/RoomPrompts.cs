using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace HotelApp1.UI.Prompts
{
    public class RoomPrompts
    {
        public static int PromptRoomSize()
        {
            var roomSize = AnsiConsole.Prompt(
              new TextPrompt<int>("[yellow]Room size(kvm):[/]")
                  .Validate(g => g < 50 && g > 10 ? ValidationResult.Success() : ValidationResult.Error("[red]Room can be minimum 10(kvm) and maximum 50(kvm)[/]"))
          );
            return roomSize;
        }
        public static int PromptRoomType()
        {
            AnsiConsole.MarkupLine("1 = Singleroom");
            AnsiConsole.MarkupLine("2 = Doubleroom");
            var roomType = AnsiConsole.Prompt(
               new TextPrompt<int>("[yellow]Room type:[/]")
                   .Validate(g => g > 0 && g <= 2? ValidationResult.Success() : ValidationResult.Error("[red]You must enter a value (1/2)[/]"))
           );
            return roomType;
        }

        public static decimal PromptPriceSquareFeet()
        {
            var roomPriceSquarefeet = AnsiConsole.Prompt(
      new TextPrompt<int>("[yellow]Price per/kvm:[/]")
          .Validate(g => g >= 50 && g <= 500? ValidationResult.Success() : ValidationResult.Error("[red]Minimum price 50kr/kvm maximum 500kr/kvm[/]"))
  );
            return roomPriceSquarefeet;
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
