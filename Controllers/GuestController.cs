using HotelApp1.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Controllers
{
    public class GuestController
    {
        public readonly GuestService _guestService;
        public GuestController(GuestService guestService)
        {
            _guestService = guestService;
        }
    }
}
