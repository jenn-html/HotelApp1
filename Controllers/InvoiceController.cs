using HotelApp1.Models;
using HotelApp1.Services;
using HotelApp1.UI.Prompts;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;

namespace HotelApp1.Controllers
{
    public class InvoiceController
    {
        private readonly InvoiceService _invoiceService;
        public InvoiceController(InvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }
        public void PayInvoice()
        {
            bool run = true;
            while (run) {
                AnsiConsole.MarkupLine("[orange1]---PAY INVOICE---[/]");
                List<Booking> bookinginvoice = _invoiceService.GetAllInvoicesFromBookings();
                ShowBookingAndInvoices(bookinginvoice);
                var invoiceId = InvoicePrompts.InvoiceIdPrompt(bookinginvoice);
                var invoiceExists = _invoiceService.GetInvoiceById(invoiceId);
                if (invoiceExists == null)
                {
                    AnsiConsole.MarkupLine($"[red]Could not find Invoice with Id {invoiceId}[/]");
                    if (AnsiConsole.Confirm("Try again with another ID?"))
                    {
                        Console.Clear();
                    }
                    else
                    {
                        run = false;
                        return;
                    }
                }
                else if (invoiceExists.IsActive == true)
                {
                    _invoiceService.PayInvoice(invoiceExists);
                    Console.Clear();
                    List<Booking> bookinginvoiceupdated = _invoiceService.GetAllInvoicesFromBookings();
                    ShowBookingAndInvoices(bookinginvoiceupdated);
                    AnsiConsole.MarkupLine($"[green] Invoice {invoiceExists.ID} payed![/]");
                    run = false;
                    return;
                }
            }
        }

        public void GetAllInvoicesFromBookings()
        {
            AnsiConsole.MarkupLine("[orange1]---LIST OF ALL INVOICES---[/]");
            List<Booking> allInvoices = _invoiceService.GetAllInvoicesFromBookings();
            if (!allInvoices.Any())
            {
                AnsiConsole.MarkupLine("[red] There are no invoices[/]");
                return; 
            }
            ShowBookingAndInvoices(allInvoices);
        }

        public void ShowDeletedInvoices(List<Invoice> invoices)
        {
            var table = new Table
            {
                Border = TableBorder.Double,
                ShowRowSeparators = true,
                ShowHeaders = true
            };


            table.AddColumn("Invoice ID");
            table.AddColumn("Created");
            table.AddColumn("Description");
            table.AddColumn("Total amount");
            table.AddColumn("Last day to pay");
            table.AddColumn("Is payed");

            foreach (var invoice in invoices)
            {
                string desc = invoice.Description != null
                    ? string.Join(", ", invoice.Description)
                    : string.Empty;

                table.AddRow(
                    Markup.Escape(invoice.ID.ToString()),
                    Markup.Escape(invoice.Created.ToString("yyyy-MM-dd")),
                    Markup.Escape(desc),
                    Markup.Escape(invoice.TotalAmount.ToString("F2")),
                    Markup.Escape(invoice.LastDayToPay.ToString("yyyy-MM-dd")),
                    Markup.Escape(invoice.IsPayed.ToString())
                );
            }

            AnsiConsole.Write(table);
        }
        public void ShowBookingAndInvoices(List<Booking> bookings)
        {
            var table = new Table
            {
                Border = TableBorder.Double,
                ShowRowSeparators = true,
                ShowHeaders = true
            };

            table.AddColumn("Invoice ID");
            table.AddColumn("Created");
            table.AddColumn("Booking ID");
            table.AddColumn("Guest name");
            table.AddColumn("Description");
            table.AddColumn("Total amount");
            table.AddColumn("Last day to pay");
            table.AddColumn("Is payed");

            foreach (var booking in bookings)
            {
                string desc = booking.Invoice.Description != null
                    ? string.Join(", ", booking.Invoice.Description)
                    : string.Empty;

                table.AddRow(
                    Markup.Escape(booking.Invoice.ID.ToString()),
                    Markup.Escape(booking.Invoice.Created.ToString("yyyy-MM-dd")),
                    Markup.Escape(booking.Id.ToString()),
                    Markup.Escape($"{booking.Guest.FirstName} {booking.Guest.LastName}"),
                    Markup.Escape(desc),
                    Markup.Escape(booking.Invoice.TotalAmount.ToString("F2")),
                    Markup.Escape(booking.Invoice.LastDayToPay.ToString("yyyy-MM-dd")),
                    Markup.Escape(booking.Invoice.IsPayed.ToString())  
                );
            }

            AnsiConsole.Write(table);
        }
        public void GetDeletedInvoices()
        {
            AnsiConsole.MarkupLine("[orange1]---LIST OF ALL DELETED INVOICES---[/]");
            List<Invoice> deletedInvoices = _invoiceService.GetAllDeletedInvoices();
            if (deletedInvoices == null )
            {
                AnsiConsole.MarkupLine("[red] There are no deleted invoices[/]");
                return;
            }
            ShowDeletedInvoices(deletedInvoices);
        }
        public Invoice CreateInvoice(Invoice invoice)
        {
            return _invoiceService.CreateInvoice(invoice);
        }

        public List<string> CreateDescription(Room selectedRoom, int numberOfNights, int numberOfExtrabeds)
        {
            var description = new List<string>();
            var nights = "night";
            var extrabeds = "Extrabeds";
            var totalpriceExtrabedsPernight = selectedRoom.PriceExtraBeds * numberOfExtrabeds;
            if (numberOfNights > 1)
            {
                nights = "nights";
                extrabeds = "Extrabed";
            }

            if (numberOfExtrabeds == 0)
            {
                description.Add($"{selectedRoom.RoomType.Type}  ");
                description.Add($"{numberOfNights} * {selectedRoom.PricePerNight} ");
                return description;
            }
            description.Add($"{selectedRoom.RoomType.Type}");
            description.Add($"{selectedRoom.PricePerNight} * {numberOfNights} {nights}");
            description.Add($"{extrabeds}");
            description.Add($"{totalpriceExtrabedsPernight} * {numberOfNights} {nights}");
            return description;
        }

        public void DeleteInvoice(Booking bookingToUpdate)
        {
            var invoiceIdToDelete = bookingToUpdate.Invoice.ID;
            var invoice = _invoiceService.GetInvoiceById(invoiceIdToDelete);
            _invoiceService.DeleteInvoice(invoice);
        }
    }
}
