using DAL.EF;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public class StatusLogRepo
    {
        private readonly ZeroHungerDbContext db;

        public StatusLogRepo(ZeroHungerDbContext db)
        {
            this.db = db;
        }

        public StatusLog Create(StatusLog log)
        {
            db.StatusLogs.Add(log);
            db.SaveChanges();
            return log;
        }

        public List<StatusLog> GetByRequestId(int requestId)
        {
            return db.StatusLogs
                     .Where(x => x.CollectRequestId == requestId)
                     .OrderBy(x => x.CreatedAt)
                     .ToList();
        }
    }
}
