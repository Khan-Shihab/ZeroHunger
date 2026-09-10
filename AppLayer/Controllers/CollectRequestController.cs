using BLL.Models;
using BLL.Services;
using DAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectRequestController : ControllerBase
    {
        CollectRequestService service;
        StatusLogRepo statusLogRepo;
        public CollectRequestController(CollectRequestService service, StatusLogRepo statusLogRepo)
        {
            this.service = service;
            this.statusLogRepo = statusLogRepo;
        }
        [HttpPost("request")]
        public IActionResult CreateRequest(CollectRequestCreateModel d)
        {
            var data = service.CreateRequest(d);
            if (data == false) return NoContent();
            return Created();
        }
        [HttpPut("{id}/assign")]
        public IActionResult AssignEmployee(int id, AssignEmployeeModel d)
        {
            var result = service.AssignEmployee(id, d);

            if (!result)
                return BadRequest();

            return Ok("Employee assigned successfully");
        }

        [HttpPut("{requestId}/collect")]
        public IActionResult CollectRequest(int requestId)
        {
            var result = service.CollectRequest(requestId);

            if (!result)
                return BadRequest("Request cannot be collected.");

            return Ok("Food collected successfully.");
        }
        [HttpPost("{requestId}/distribute")]
        public IActionResult DistributeRequest(int requestId, DistributeModel model)
        {
            var result = service.DistributeRequest(requestId, model);

            if (!result)
                return BadRequest("Request cannot be distributed.");

            return Ok("Food distributed successfully.");
        }

        [HttpPut("{requestId}/complete")]
        public IActionResult CompleteRequest(int requestId)
        {
            var result = service.CompleteRequest(requestId);

            if (!result)
                return BadRequest("Request cannot be completed.");

            return Ok("Request completed successfully.");
        }
        [HttpGet("{requestId}/status-logs")]
        public IActionResult GetStatusLogs(int requestId)
        {
            var logs = statusLogRepo.GetByRequestId(requestId);
            return Ok(logs);
        }
        [HttpGet("totalbeneficiary")]
        public IActionResult TotalBeneficiary()
        {
            var TotalCount = service.TotalBeneficiary();
            return Ok(TotalCount);
        }
        [HttpGet("getall")]
        public IActionResult getall()
        {
            var data = service.GetAll();
            return Ok(data);
        }
        [HttpGet("pendingrequest")]
        public IActionResult PendingRequests()
        {
            var data = service.GetPendingRequests();
            return Ok(data);
        }
        [HttpGet("completerequest")]
        public IActionResult completerequest()
        {
            var data = service.GetCompleteRequests(); 
            return Ok(data);
        }
        [HttpGet("RequestAdditionalInfo")]
        public IActionResult RequestAdditionalInfo()
        {
            var data = service.RequestAdditionalInfo();
            return Ok(data);
        }
        [HttpGet("GetDistributionReport")]
        public IActionResult GetDistributionReport()
        {
            var data = service.GetDistributionReport();
            return Ok(data);
        }
        [HttpGet("EmployeeDistributionSummary")]
        public IActionResult EmployeeDistributionSummary()
        {
            var data = service.EmployeeDistributionSummary();
            return Ok(data);
        }
    }
}
