using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DAL.Repository
{
    public class CollectRequestRepo
    {
        ZeroHungerDbContext db;
        public CollectRequestRepo(ZeroHungerDbContext db)
        {
            this.db = db;
        }

        public CollectRequest Create(CollectRequest req)
        {
            db.CollectRequests.Add(req);
            db.SaveChanges();
            return req;
        }

        public List<CollectRequest> GetAll()
        {
            var data = db.CollectRequests.ToList();
            return data;
        }

        public CollectRequest? GetById(int id)
        {
            var data = db.CollectRequests.Find(id);
            return data;
        }

        public bool Update(CollectRequest req)
        {
            var data = GetById(req.Id);
            if (data == null) return false;

            data.Status = req.Status;
            data.EmployeeId = req.EmployeeId;
            data.PickupNotes = req.PickupNotes;
            data.MaxPreserveUntil = req.MaxPreserveUntil;
            data.AcceptedAt = req.AcceptedAt;
            data.CollectedAt = req.CollectedAt;
            data.DistributedAt = req.DistributedAt;
            data.CompletedAt = req.CompletedAt;

            db.SaveChanges();
            return true;
        }
        public List<CollectRequest> GetPendingRequests()
        {
            var data = (from s in db.CollectRequests
                       where s.Status == "PENDING"
                       select s).ToList();
            return data;
        }
        public List<CollectRequest> GetCompleteRequests()
        {
            var data = (from s in db.CollectRequests
                        where s.Status == "COMPLETED"
                        select s).ToList();
            return data;
        }

        public object RequestAdditionalInfo()
        {
            var data = from s in db.CollectRequests.Include(s => s.Restaurant).Include(s => s.Employee)
                       select new
                       {
                           ReqId = s.Id,
                           Status = s.Status,
                           RestName = s.Restaurant.User.Name,
                           EmpName = s.Employee != null ? s.Employee.User.Name: "Not Assigned"
                       };

            return data;
        }

       
       

    }
}