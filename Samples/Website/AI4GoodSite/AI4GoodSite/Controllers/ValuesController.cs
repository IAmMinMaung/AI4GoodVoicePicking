using AI4GoodSite.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AI4GoodSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private ApplicationDbContext _context;
        public ValuesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult GetResult()
        {
            var list = _context.Roles.ToList();
            if(list.Any())
                return Ok(list);
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(string id)
        {
            try
            {
                var result = _context.Users.FirstOrDefault(u => u.Id == id);
                if (result == null)
                    return NotFound();
                else
                    return new ObjectResult(result);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}