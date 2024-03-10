using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ScanAndGoApi.Context;
using ScanAndGoApi.Models;

namespace ScanAndGoApi.ApiController
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpPost("CreateProduct")]
        public IActionResult Create([FromBody] Product product)
        {
            using (var context = new DatabaseContextFactory().CreateDbContext(null))
            {
                try
                {
                    context.Products.Add(product);
                    context.SaveChanges();
                    return new OkObjectResult(product);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return new BadRequestObjectResult(e.Message);
                }
            }
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                using var context = new DatabaseContextFactory().CreateDbContext(null);
                return new OkObjectResult(context.Products.ToList());
            } 
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
        [HttpPost("GetImageById")]
        public IActionResult GetImageById([FromBody] long id)
        {
            try
            {
                using var context = new DatabaseContextFactory().CreateDbContext(null);
                var product = context.Products.FirstOrDefault(p => p.id == id);
                if (product == null)
                {
                    return new NotFoundObjectResult("Product not found");
                }
                if (product.Image == null)
                {
                    return new NotFoundObjectResult("Image not found");
                }
                var file = File.ReadAllBytes(@product.Image);
                return new FileContentResult(file, "image/jpeg");
               
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

    }
}
