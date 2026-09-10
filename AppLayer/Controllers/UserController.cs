using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace AppLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserService services;
        public UserController(UserService services)
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
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = services.Delete(id);
            if (result == false) return NotFound();
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult Update (UserUpdateModel d, int id)
        {
            var result = services.Update(d, id);
            if (result == false) return NotFound();
            return Ok();
        }


    }
}
