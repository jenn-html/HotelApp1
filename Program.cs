using HotelApp1;
using HotelApp1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace HotelApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            var app = new Application();
            app.Run();
        }
    }
}
