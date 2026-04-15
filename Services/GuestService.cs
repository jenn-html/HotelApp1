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
        public void AddNewGuest(Guest guest)
        {
            _guestRepository.Add(guest);
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

        public void AddDeletedGuest(Guest guestToAdd)
        {
            _guestRepository.AddDeletedGuest(guestToAdd);
        }

        public List<Guest> GetAllActiveGuests()
        {
            return _guestRepository.GetAllActiveGuests();
        }

        public List<Guest> GetAllDeletedGuests()
        {
            return _guestRepository.GetAllDeletedGuests();
        }

        public List<Guest> GetAllGuestsToday()
        {
            return _guestRepository.GetAllGuestsToday();
        }

        public void UpdateGuest(Guest existingGuest, Guest updatedGuest)
        {
           _guestRepository.Update(existingGuest, updatedGuest);
        }
    }
}
