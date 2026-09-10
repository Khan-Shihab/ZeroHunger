using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        EmployeeService services;
        public EmployeeController(EmployeeService services)
        {
            this.services = services;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var data = services.GetAll();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = services.GetbyId(id);
            return Ok(data);
        }       
        
        [HttpPut("{id}")]
        public IActionResult Update(EmployeeUpdateModel d, int id)
        {
            var result = services.Update(d, id);
            if (result == false) return NotFound();
            return Ok();
        }
        [HttpGet("GetAvailableEmployees")]
        public IActionResult GetAvailableEmployees()
        {
            var result = services.GetAvailableEmployees();
            return Ok(result);
        }
    }
}
