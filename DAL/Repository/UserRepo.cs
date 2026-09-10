using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public class UserRepo
    {
        ZeroHungerDbContext db;
        public UserRepo(ZeroHungerDbContext db)
        {
            this.db= db;
        }
        public bool Create(User user)
        {
            db.Users.Add(user);
            return db.SaveChanges() > 0;
        }
        public List<User> GetAll()
        {
            var data = db.Users.ToList();
            return data;
        }
        public bool IsEmailTaken(string email)
        {
            return db.Users.Any(u => u.Email == email);
        }
        public User? GetByEmail(string email)
        {
            return db.Users.FirstOrDefault(u => u.Email == email);
        }
        public User? GetById(int id)
        {
            var data = db.Users.Find(id);
            return data;
        }
        public bool Update(User user,int id)
        {
            var data = GetById(id);
            if (data == null) { return false; } 
            data.Name = user.Name;
            data.Role = user.Role;
            data.Email = user.Email;
            data.Phone = user.Phone;
            data.IsActive = user.IsActive;
            return db.SaveChanges() > 0;
        }
        public int AdminUseId()
        {
            var data = (from s in db.Users
                       where s.Role == "ADMIN"
                       select s.Id).FirstOrDefault();
            return data;
        }

       
    }
}
