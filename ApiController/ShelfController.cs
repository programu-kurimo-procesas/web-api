
using Microsoft.AspNetCore.Mvc;
using ScanAndGoApi.Context;
using ScanAndGoApi.Models;
using ScanAndGoApi.Dtos;
namespace ScanAndGoApi.ApiController
{
    [ApiController]
    [Route("[controller]")]
    public class ShelfController
    {
        private readonly ILogger<ShelfController> _logger;

        public ShelfController(ILogger<ShelfController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetAllByStoreId/{id}")]
        public IActionResult GetAllByStoreId([FromRoute] int id)
        {
            try
            {
                using var context = new DatabaseContextFactory().CreateDbContext(null);
                var shelves = context.Shelves.Where(s => s.Store.Id == id).ToList();
                return new OkObjectResult(shelves);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpGet("GetByProductIdAndStoreId/{productId}/{storeId}")]
        public IActionResult GetByProductIdAndStoreId([FromRoute] long productId, [FromRoute] int storeId)
        {
            try
            {
                using var context = new DatabaseContextFactory().CreateDbContext(null);
                Shelf? shelf = context.Shelves
    .Where(s => s.Items.Any(i => i.Product.id == productId) && s.Store.Id == storeId)
    .FirstOrDefault();

                if (shelf is null)
                {
                    return new BadRequestObjectResult(new ErrorResponse() { Error="No blet item"});
                }
                return new OkObjectResult(shelf.Id);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}
