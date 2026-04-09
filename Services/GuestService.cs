using HotelApp1.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Services
{
    public class GuestService : ICrud
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

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
