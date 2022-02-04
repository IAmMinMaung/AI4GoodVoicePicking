using AI4GoodSite.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AI4GoodSite.Controllers
{
    /// <summary>
    /// A Controller class for exposing Picking related Endpoints
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PickingController : ControllerBase
    {
        private ApplicationDbContext _context;
        public PickingController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns all the items available in the database
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllItems()
        {
            var itemsList = _context.Items.ToList();
            if(itemsList.Any())
                return Ok(itemsList);
            return NotFound();    
        }

        /// <summary>
        /// Returns top 5 items that are unscanned, if available
        /// </summary>
        /// <returns></returns>
        public IActionResult GetItemsToPick()
        {
            try
            {
                var itemScansIds = _context.ItemScans.Select(itemScan => itemScan.ItemId).ToList();
                var itemsUnscanned = _context.Items.Where(item => !itemScansIds.Contains(item.ItemId)).Take(5).ToList();
                
                if (itemsUnscanned == null)
                    return NotFound();
                else
                    return new ObjectResult(itemsUnscanned);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
