using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public class DistributionRecordRepo
    {
        ZeroHungerDbContext db;
        public DistributionRecordRepo(ZeroHungerDbContext db)
        {
            this.db = db;
        }
        public DistributionRecord Create(DistributionRecord record)
        {
            db.DistributionRecords.Add(record);
            db.SaveChanges();
            return record;
        }
        public List<DistributionRecord> GetAll()
        {
            var data = db.DistributionRecords.ToList();
            return data;
        }
        public DistributionRecord? GetById(int id)
        {
            var data = db.DistributionRecords.Find(id);
            return data;
        }
        public bool Update(DistributionRecord employee)
        {
            var data = GetById(employee.Id);
            if (data == null) return false;

            data.DistributionPoint = employee.DistributionPoint;
            data.BeneficiaryCount = employee.BeneficiaryCount;
            data.Notes = employee.Notes;
            return db.SaveChanges() > 0;
        }
        public object GetDistributionReport()
        {
            var data = (from d in db.DistributionRecords.Include(d=>d.CollectRequest)
                       select new
                       {
                           RequestId = d.CollectRequestId,
                           RestaurantName = d.CollectRequest.Restaurant.User.Name,
                           EmployeeId = d.CollectRequest.EmployeeId,
                           EmployeeName = d.CollectRequest.Employee.User.Name,
                           DistributionPoint = d.DistributionPoint,
                           BeneficiaryCount = d.BeneficiaryCount,
                           Notes = d.Notes,
                           DistributedAt = d.CollectRequest.DistributedAt
                       }).ToList();

            return data;
        }
        public object EmployeeDistributionSummary()
        {
            var data = db.DistributionRecords
                .GroupBy(x => new
                {
                    x.CollectRequest.Employee.User.Name,
                    x.CollectRequest.EmployeeId
                })
                .Select(g => new
                {
                    EmployeeId = g.Key.EmployeeId,
                    EmployeeName = g.Key.Name,
                    TotalDistributed = g.Count(),
                    TotalBeneficiaries = g.Sum(x => x.BeneficiaryCount)
                })
                .OrderByDescending(x => x.TotalBeneficiaries)
                .ToList();

            return data;
        }
        public int TotalBeneficiary()
        {
            var totalBeneficiary = (from s in db.DistributionRecords
                                    select s.BeneficiaryCount).Sum();
            return totalBeneficiary;
        }
    }
}
