using DAL.EF;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public class FoodItemRepo
    {
        ZeroHungerDbContext db;
        public FoodItemRepo(ZeroHungerDbContext db)
        {
            this.db = db;
        }
        public bool Create(FoodItem user)
        {
            db.FoodItems.Add(user);
            return db.SaveChanges() > 0;
        }
        public List<FoodItem> GetAll()
        {
            var data = db.FoodItems.ToList();
            return data;
        }
        public FoodItem? GetById(int id)
        {
            var data = db.FoodItems.Find(id);
            return data;
        }

        public List<FoodItem> GetByRequestId(int requestId)
        {
            var data = (from d in db.FoodItems
                        where d.CollectRequestId == requestId
                        select d).ToList();
            return data;
        }


    }
}
