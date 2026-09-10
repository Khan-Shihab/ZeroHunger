using AutoMapper;
using BLL.Models;
using DAL.EF.Tables;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace BLL.Services
{
    public class CollectRequestService
    {
        CollectRequestRepo Requestrepo;
        FoodItemRepo foodrepo;
        EmployeeRepo emprepo;
        DistributionRecordRepo Distributerepo;
        StatusLogRepo statuslogrepo;
        RestaurantRepo restaurantrepo;
        UserRepo userrepo;
        IMapper mapper;
        public CollectRequestService(IMapper mapper,CollectRequestRepo Requestrepo, FoodItemRepo foodrepo, EmployeeRepo emprepo, DistributionRecordRepo Distributerepo, StatusLogRepo statuslogrepo, RestaurantRepo restaurantrepo, UserRepo userrepo)
        {
            this.mapper = mapper;
            this.Requestrepo = Requestrepo;
            this.foodrepo = foodrepo;
            this.emprepo = emprepo;
            this.Distributerepo = Distributerepo;
            this.statuslogrepo = statuslogrepo;
            this.restaurantrepo = restaurantrepo;
            this.userrepo = userrepo;
        }

        private void AddStatusLog(int requestId, string? oldStatus, string newStatus, int changedBy, string? note)
        {
            var log = new StatusLog()
            {
                CollectRequestId = requestId,
                OldStatus = oldStatus,
                NewStatus = newStatus,
                ChangedBy = changedBy,
                Note = note,
                CreatedAt = DateTime.Now
            };

            statuslogrepo.Create(log);
        }

        public bool CreateRequest(CollectRequestCreateModel d)
        {
            var RestData = restaurantrepo.GetById(d.RestaurantId);
            if (RestData == null) return false;
            var data = new CollectRequest()
            {
                RestaurantId = d.RestaurantId, 
                MaxPreserveUntil = d.MaxPreserveUntil,
                PickupNotes = d.PickupNotes,
                Status = "PENDING",
                CreatedAt = DateTime.Now,             
            };
            var request = Requestrepo.Create(data);


            foreach (var item in d.FoodItems)
            {
                var footItem = new FoodItem()
                {
                    CollectRequestId = request.Id,
                    Name = item.Name,
                    Quantity = item.Quantity,
                    Unit = item.Unit,
                    Category = item.Category,
                };
                foodrepo.Create(footItem);
            }

           
            AddStatusLog(request.Id, null, request.Status, RestData.UserId, "Request created");

            return true;
        }
        public bool AssignEmployee(int requestId, AssignEmployeeModel employeeId)
        {
            var request = Requestrepo.GetById(requestId);

            if (request == null)
                return false;

            if (request.Status != "PENDING")
                return false;

            var employee = emprepo.GetById(employeeId.EmployeeId);

            if (employee == null)
                return false;

            if (employee.Availability != "AVAILABLE")
                return false;

            request.EmployeeId = employeeId.EmployeeId;
            request.Status = "ACCEPTED";
            request.AcceptedAt = DateTime.Now;

            employee.Availability = "BUSY";

            var requestUpdated = Requestrepo.Update(request);
            var employeeUpdated = emprepo.Update(employee);

            var adminUserId = userrepo.AdminUseId();

            AddStatusLog(request.Id, "PENDING", "ACCEPTED",adminUserId, "Employee assigned");

            return true;
        }

        public bool CollectRequest(int requestId)
        {
            var request = Requestrepo.GetById(requestId);
            if (request == null) return false;
            if (request.Status != "ACCEPTED") return false;
            var employeeinfo = emprepo.GetById(request.EmployeeId!.Value);
            if (employeeinfo == null)  return false;

            var employeeUser = userrepo.GetById(employeeinfo.UserId);
            if (employeeUser == null) return false;

            var oldStatus = request.Status;

            request.Status = "COLLECTED";
            request.CollectedAt = DateTime.Now;
            var isOk = Requestrepo.Update(request);
            if (!isOk) return false;

            AddStatusLog(request.Id,oldStatus, "COLLECTED",employeeUser.Id,"Food collected");
            return true;
        }
        public bool DistributeRequest(int requestId, DistributeModel d)
        {
            var request = Requestrepo.GetById(requestId);

            if (request == null)
                return false;

            if (request.Status != "COLLECTED")
                return false;

            var employeeinfo = emprepo.GetById(request.EmployeeId!.Value);

            if (employeeinfo == null)
                return false;

            var employeeUser = userrepo.GetById(employeeinfo.UserId);

            if (employeeUser == null)
                return false;

            var record = new DistributionRecord()
            {
                CollectRequestId = requestId,
                DistributionPoint = d.DistributionPoint,
                BeneficiaryCount = d.BeneficiaryCount,
                Notes = d.Notes
            };

            var distribution = Distributerepo.Create(record);

            if (distribution == null)
                return false;

            var oldStatus = request.Status;

            request.Status = "DISTRIBUTED";
            request.DistributedAt = DateTime.Now;

            var isOk = Requestrepo.Update(request);

            if (!isOk)
                return false;

            AddStatusLog( request.Id,oldStatus, "DISTRIBUTED", employeeUser.Id,"Food distributed");

            return true;
        }
        public bool CompleteRequest(int requestId)
        {
            var request = Requestrepo.GetById(requestId);
            if (request == null) return false;
            if (request.Status != "DISTRIBUTED") return false;

            var employee = emprepo.GetById(request.EmployeeId!.Value);
            if (employee == null) return false;

            var employeeUser = userrepo.GetById(employee.UserId);
            if (employeeUser == null)return false;

            var oldStatus = request.Status;

            request.Status = "COMPLETED";
            request.CompletedAt = DateTime.Now;

            employee.Availability = "AVAILABLE";

            var requestUpdated = Requestrepo.Update(request);
            var employeeUpdated = emprepo.Update(employee);
            if (!requestUpdated || !employeeUpdated) return false;
            AddStatusLog(request.Id, oldStatus,"COMPLETED",employeeUser.Id, "Request completed");
            return true;
        }
        public List<CollectRequestModel> GetAll()
        {
            var data = Requestrepo.GetAll();
            var mapped = mapper.Map<List<CollectRequestModel>>(data);
            return mapped;
        }

        public int TotalBeneficiary()
        {
            var data = Distributerepo.TotalBeneficiary();
            return data;
        }
        public object GetDistributionReport()
        {
            var data = Distributerepo.GetDistributionReport();
            return data;
        }
        public List<CollectRequestModel> GetPendingRequests()
        {
            var data = Requestrepo.GetPendingRequests();
            var mapped = mapper.Map<List<CollectRequestModel>>(data);
            return mapped;
        }
        public List<CollectRequestModel> GetCompleteRequests()
        {
            var data = Requestrepo.GetCompleteRequests();
            var mapped = mapper.Map<List<CollectRequestModel>>(data);
            return mapped;
        }
        public object RequestAdditionalInfo()
        {
            var data = Requestrepo.RequestAdditionalInfo();
            return data;
        }


        public object EmployeeDistributionSummary()
        {
            var data = Distributerepo.EmployeeDistributionSummary();
            return data;
        }
    }
}
