using HotelApp1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Repositories
{
    public interface IInvoiceRepository
    {
        List<Invoice> GetAll();
        Invoice? GetById(int id);
        void Add(Invoice invoice); 
        void Delete(int id);
        void Update(Invoice invoice);
        
    }
}
