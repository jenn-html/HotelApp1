using HotelApp1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Utilities.Interfaces
{
    public interface ICRUDInvoice
    {
        public Invoice CreateInvoice(Invoice invoice);
        public Invoice GetInvoiceById(int invoiceId);
        public void PayInvoice(Invoice invoiceToPay);
        public List<Invoice> GetAllDeletedInvoices();
        public void DeleteInvoice(Invoice invoiceToDelete);
        public List<Booking> GetAllInvoicesFromBookings();
    }
}
