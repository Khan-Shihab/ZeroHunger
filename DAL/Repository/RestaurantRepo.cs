using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public class RestaurantRepo
    {
        ZeroHungerDbContext db;
        public RestaurantRepo(ZeroHungerDbContext db)
        {
            this.db = db;
        }
        public bool Create(Restaurant user)
        {
            db.Restaurants.Add(user);
            return db.SaveChanges() > 0;
        }
        public List<Restaurant> GetAll()
        {
            var data = db.Restaurants.ToList();
            return data;
        }
        public Restaurant? GetById(int id)
        {
            var data = db.Restaurants.Find(id);
            return data;
        }
        public bool Update(Restaurant restaurant,int id)
        {
            var data = GetById(id);
            if (data == null) return false;

            data.Address = restaurant.Address;
            data.ContactPerson = restaurant.ContactPerson;
            data.IsVerified = restaurant.IsVerified;
            return db.SaveChanges() > 0;
        }
        public object ResturentWithCollectionReq()
        {
            var data = from s in db.Restaurants.Include(s => s.CollectRequests)
                       select new
                       {
                           ResturentID = s.Id,
                           RestaurantName = s.User.Name,
                           CollectionReq = s.CollectRequests
                       };
            return data.ToList();

        }
    }
}
