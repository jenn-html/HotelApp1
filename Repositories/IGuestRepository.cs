using HotelApp1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Repositories
{
    public interface IGuestRepository
    {
        List<Guest> GetAll();
        Guest GetById(int id);
        void Add(Guest guest);
        void Update(Guest guest, Guest guest1);
        void DeleteGuest(Guest guest);
    }
}
