using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Services
{
    internal interface ICrud
    {
        void Create();
        void Read();
        void Update();
        void Delete();
    }
}
