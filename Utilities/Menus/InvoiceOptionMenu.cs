using HotelApp1.Controllers;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Utilities.Menus
{
    public class InvoiceOptionMenu : IMenu
    {
        public readonly InvoiceController _invoiceController;
        public InvoiceOptionMenu(InvoiceController invoiceController)
        {
            _invoiceController = invoiceController;
        }
        public void ShowMenu()
        {
            string[] options = new[] {
                $"[orange1]SHOW ALL INVOICES[/]",
                $"[orange1]SHOW DELETED INVOICES[/]",
                $"[orange1]PAY INVOICE[/]",
                $"[yellow]GO BACK TO MAIN MENU[/]"
            };

            while (true)
            {
                Console.Clear();

                // Use Spectre.Console SelectionPrompt for keyboard navigation + Enter selection
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[orange1]---INVOICE MENU---[/]")
                        .PageSize(4)
                        .AddChoices(options)
                        .WrapAround()
                );

                int selected = Array.IndexOf(options, choice);

                Console.Clear();
                switch (selected)
                {
                    case 0:
                        _invoiceController.GetAllInvoicesFromBookings();

                        break;
                    case 1:
                        _invoiceController.GetDeletedInvoices();

                        break;
                    case 2:
                        _invoiceController.PayInvoice();
                        break;
                    case 3:
                        return;
                    default:
                        AnsiConsole.MarkupLine("[red]Invalid option[/]");
                        break;
                }
                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey(true);
            }
        }
    }
}
