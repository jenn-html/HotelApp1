using HotelApp1.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.UI.Prompts
{
    public class InvoicePrompts
    {
        public static int InvoiceIdPrompt(List<Booking> booking)
        {
            var invoiceID = AnsiConsole.Prompt(
              new TextPrompt<int>("[yellow]Enter Invoice ID to pay:[/]")
                  .Validate(g => g > 0 ? ValidationResult.Success() : ValidationResult.Error("[red]Minimum 1 character [/]"))
          );
            return invoiceID;
        }
    }
}
