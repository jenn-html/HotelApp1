using HotelApp1.Data;
using HotelApp1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp1.Repositories
{
    public class GuestRepository : IGuestRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public GuestRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public void Add(Guest guest)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Guest> GetAll()
        {
            throw new NotImplementedException();
        }

        public Guest GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Guest guest)
        {
            throw new NotImplementedException();
        }
    }
}
