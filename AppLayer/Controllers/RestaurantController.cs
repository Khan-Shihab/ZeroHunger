using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        RestaurantService services;
        public RestaurantController(RestaurantService services)
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
        public IActionResult Update(RestaurantUpdateModel d, int id)
        {
            var result = services.Update(d, id);
            if (result == false) return NotFound();
            return Ok();
        }

        [HttpGet("ResturentWithCollectionReq")]
        public IActionResult ResturentWithCollectionReq()
        {
            var data = services.ResturentWithCollectionReq();
            return Ok(data);
        }
    }
}
