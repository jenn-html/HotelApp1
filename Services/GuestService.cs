using HotelApp1.Models;
using HotelApp1.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Services
{
    public class GuestService 
    {
        public readonly GuestRepository _guestRepository;
        public GuestService(GuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }
        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            _guestRepository.DeleteById(id);
        }

        public List<Guest> GetAllGuests()
        {
            return _guestRepository.GetAll();
        }
        public Guest GetById(int id)
        {
            return _guestRepository.GetById(id);
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
