using Microsoft.AspNetCore.Mvc;
using ScanAndGoApi.Context;

namespace ScanAndGoApi.ApiController
{

    [ApiController]
    [Route("[controller]")]
    public class StoreController
    {
        private readonly ILogger<StoreController> _logger;

        public StoreController(ILogger<StoreController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                using var context = new DatabaseContextFactory().CreateDbContext(null);
                var stores = context.Stores.ToList();
                return new OkObjectResult(stores);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpGet("GetMap/{id}")]
        public IActionResult GetImageById([FromRoute] long id)
        {
            try
            {
                using var context = new DatabaseContextFactory().CreateDbContext(null);
                var store = context.Stores.FirstOrDefault(p => p.Id == id);
                if (store == null)
                {
                    return new NotFoundObjectResult("Store not found");
                }
                if (store.MapUrl == null)
                {
                    return new NotFoundObjectResult("Image not found");
                }
                var file = File.ReadAllBytes(store.MapUrl);
                return new FileContentResult(file, "image/jpeg");

            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}
