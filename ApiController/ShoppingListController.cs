using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScanAndGoApi.Context;
using ScanAndGoApi.Dtos;
using ScanAndGoApi.Models;

namespace ScanAndGoApi.ApiController
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingListController
    {
        private readonly ILogger<ShoppingListController> _logger;

        public ShoppingListController(ILogger<ShoppingListController> logger)
        {
            _logger = logger;
        }

        [HttpPost("GetAllByUser")]
        public IActionResult GetAllByUser([FromBody] int userid)
        {
            try
            {
                using var context = new DatabaseContextFactory().CreateDbContext(null);
                var shoppingLists = context.ShoppingLists.Where(sl => sl.User.id == userid).ToList();
                return new OkObjectResult(shoppingLists);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
        [HttpGet("GetAllProductsById/{id}")]
        public IActionResult GetAllProductsById([FromRoute] int id)
        {
            try
            {
                using var context = new DatabaseContextFactory().CreateDbContext(null);
                var products = context.ProductListAsc.Where(pla => pla.ShoppingList.Id == id).Select(pla => pla.Product).ToList();
                return new OkObjectResult(products);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
        [HttpDelete("RemoveProductFromList")]
        public IActionResult RemoveProductFromList([FromBody] ProductToCartDto dto)
        {
            try
            {
                using var context = new DatabaseContextFactory().CreateDbContext(null);
                var product = context.ProductListAsc.Where(pla => pla.ShoppingList.Id == dto.UserId && pla.Product.id == dto.ProductId).FirstOrDefault();
                if (product == null)
                {
                    return new NotFoundObjectResult("No such product in list");
                }
                context.ProductListAsc.Remove(product);
                context.SaveChanges();
                return new OkObjectResult(product);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpPost("AddProductToList")]
        public IActionResult AddProductToList([FromBody] ProductToCartDto dto)
        {
            Product? product;
            ShoppingList? shoppingList;
            try
            {
                using var context = new DatabaseContextFactory().CreateDbContext(null);
                product = context.Products.Where(p => p.id == dto.ProductId).FirstOrDefault();
                if (product == null)
                {
                    return new BadRequestObjectResult("No such product with given id");
                }
                shoppingList = context.ShoppingLists.Where(sl => sl.User.id == dto.UserId).FirstOrDefault();
                if (shoppingList == null)
                {
                    return new BadRequestObjectResult("No such shopping list with given id");
                }
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
            try
            {
                var productInList = new ProductListAsc
                {
                    Product = product,
                    ShoppingList = shoppingList
                };

                using var context = new DatabaseContextFactory().CreateDbContext(null);
                //find if there exists one with the same product and shopping list
                List<ProductListAsc> plas = context.ProductListAsc.Where(pla => pla.Product.id == product.id && pla.ShoppingList.Id == shoppingList.Id).ToList();
                if (plas.Count > 0)
                {
                    return new BadRequestObjectResult("Product already in list");
                }
                context.Products.Attach(productInList.Product);
                context.ShoppingLists.Attach(productInList.ShoppingList);
                context.ProductListAsc.Add(productInList);
                context.SaveChanges();
                return new OkObjectResult(product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpGet("GetQuantityInStore/{id}/{storeId}")]
        public IActionResult GetQuantityInStore([FromRoute] int id, [FromRoute] int storeId)
        {

            using var context = new DatabaseContextFactory().CreateDbContext(null);

            var shoppingList = context.ProductListAsc.Include(pla => pla.Product).Where(pla => pla.ShoppingList.Id == id).ToList();
            if (shoppingList == null)
            {
                return new NotFoundObjectResult("No such shopping list");
            }
            int quantity = 0;
            foreach (var product in shoppingList)
            {
                var item = context.Items.Where(i => i.Product.id == product.Product.id && i.Shelf.Store.Id == storeId).FirstOrDefault();
                if (item != null)
                {
                    quantity++;
                }
            }
            return new OkObjectResult(new Quantity() { quantity = quantity });
        }
    }

}
