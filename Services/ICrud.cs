using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Services
{
    public interface ICrud
    {
        void Create();
        void Read();
        void Update();
        void Delete();
    }
}
