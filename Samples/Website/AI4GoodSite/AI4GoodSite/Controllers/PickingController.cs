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
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]/[action]")]
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

        [HttpGet("{itemId}")]
        public IActionResult UpdatePickedItemById(int itemId)
        {
            try
            {
                if (_context.ItemScans.Any(itemScan => itemScan.ItemId == itemId))
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                // https://localhost:44306/api/picking/UpdatePickedItembyId/1
                var itemScanCount = _context.ItemScans.Count();
                var orderId = _context.Orders.Where(o => itemId >= o.ItemIdStart && itemId <= o.ItemIdEnd).FirstOrDefault()?.OrderId;
                _context.ItemScans.Add(new ItemScan() { ItemScanId = itemScanCount+1, ItemId = itemId, ScannedDateTime = DateTime.Now, User = "AI4GoodUser", OrderId = orderId ?? 1 });
                _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public class UpdateRequest
        {
            public int ItemId { get; set; } 
            public int OrderId { get; set; }    
        }
    }
}
