using HotelApp1.Data;
using HotelApp1.Models;
using HotelApp1.Utilities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Repositories
{
    public class InvoiceRepository : ICRUDInvoice
    {
        private readonly ApplicationDbContext _context;
        public InvoiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Booking> GetAllInvoicesFromBookings()
        {
            return _context.Bookings
                .Include(b => b.Invoice)
                .Include(b => b.Guest).Where(b => b.Invoice.IsActive == true)
                .ToList();
        }
        public List <Invoice> GetAllDeletedInvoices()
        {
            return _context.Invoices
                .Where(i => i.IsActive == false)
                .ToList();
        }

        public void PayInvoice(Invoice invoiceToPay)
        {
            if (invoiceToPay.IsActive == false)
            {
                AnsiConsole.MarkupLine("No invoice found");
                return;
            }
            else
            {
                invoiceToPay.IsPayed = true;
                AnsiConsole.MarkupLine($"[green] Invoice successfully payed! [/]");
                _context.SaveChanges();
            }
        }

        public Invoice GetInvoiceById(int invoiceId)
        {
            return _context.Invoices
                .Where(i => i.ID == invoiceId)
                .FirstOrDefault();
        }
        public Invoice CreateInvoice(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            _context.SaveChanges();
            return invoice;
        }

        public void DeleteInvoice(Invoice invoiceToDelete)
        {
            invoiceToDelete.IsActive = false;
            _context.SaveChanges();
        }
    }
}
