using HotelApp1.Models;
using HotelApp1.Repositories;
using HotelApp1.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Services
{
    public class InvoiceService : ICRUDInvoice
    {
        private readonly InvoiceRepository _invoiceRepository;
        public InvoiceService(InvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        public List<Invoice> GetAllDeletedInvoices()
        {
            return _invoiceRepository.GetAllDeletedInvoices();
        }

        public List<Booking> GetAllInvoicesFromBookings()
        {
            return _invoiceRepository.GetAllInvoicesFromBookings();
        }

        public void PayInvoice(Invoice invoiceToPay)
        {
            _invoiceRepository.PayInvoice(invoiceToPay);
        }

        public Invoice GetInvoiceById(int invoiceId)
        {
            return _invoiceRepository.GetInvoiceById(invoiceId);
        }

        public Invoice CreateInvoice(Invoice invoice)
        {
            return _invoiceRepository.CreateInvoice(invoice);
        }

        public void DeleteInvoice(Invoice invoiceToDelete)
        {
            _invoiceRepository.DeleteInvoice(invoiceToDelete);
        }
    }
}
