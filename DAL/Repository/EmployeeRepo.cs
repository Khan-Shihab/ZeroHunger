using DAL.EF;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public class EmployeeRepo
    {
        ZeroHungerDbContext db;
        public EmployeeRepo(ZeroHungerDbContext db)
        {
            this.db = db;
        }
        public bool Create(Employee user)
        {
            db.Employees.Add(user);
            return db.SaveChanges() > 0;
        }
        public List<Employee> GetAll()
        {
            var data = db.Employees.ToList();
            return data;
        }
        public Employee? GetById(int id)
        {
            var data = db.Employees.Find(id);
            return data;
        }
        public bool Update(Employee employee)
        {
            var data = GetById(employee.Id);
            if (data == null) return false;

            data.Phone = employee.Phone;
            data.CoverageArea = employee.CoverageArea;
            data.Availability = employee.Availability;
            db.SaveChanges();
            return true;
        }

        public List<Employee> GetAvailableEmployees()
        {
            var data = (from d in db.Employees
                        where d.Availability== "AVAILABLE"
                        select d).ToList();
            return data;
        }
        

    }
}
