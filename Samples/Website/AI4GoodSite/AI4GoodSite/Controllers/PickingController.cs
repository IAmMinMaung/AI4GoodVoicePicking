using AI4GoodSite.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet("{itemIds}")]
        public async Task<IActionResult> UpdatePickedItemsByIds(string itemIds)
        {
            try
            {
                var itemIdList = itemIds.Split(",");
                var itemScans = new List<ItemScan>();
                var itemScanCount = _context.ItemScans.Count();
                int i = 1;
                foreach (var id in itemIdList)
                {
                    var itemId = Int32.Parse(id);
                    if (_context.ItemScans.Any(itemScan => itemScan.ItemId == itemId))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest);
                    }
                    var itemScanId = itemScanCount + i++;
                    UpdatePickedItemById(itemId, itemScanId, ref itemScans);
                }
                await _context.ItemScans.AddRangeAsync(itemScans);
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private void UpdatePickedItemById(int itemId, int itemScanId, ref List<ItemScan> itemScans)
        {
            // https://localhost:44306/api/picking/UpdatePickedItembyId/1
            
            var orderId = _context.Orders.Where(o => itemId >= o.ItemIdStart && itemId <= o.ItemIdEnd).FirstOrDefault()?.OrderId;
            itemScans.Add(new ItemScan() { ItemScanId = itemScanId, ItemId = itemId, ScannedDateTime = DateTime.Now, User = "AI4GoodUser", OrderId = orderId ?? 1 });
        }

        public class UpdateRequest
        {
            public int ItemId { get; set; } 
            public int OrderId { get; set; }    
        }
    }
}
