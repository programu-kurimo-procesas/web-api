using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
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
                return new OkObjectResult(context.ShoppingLists.Where(sl => sl.User.id == userid).ToList());
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpPost("CreateShoppingList")]
        public IActionResult Create([FromBody] int userid)
        {
            User? user;
            try
            {
                using var context = new DatabaseContextFactory().CreateDbContext(null);
                user = context.Users.Where(u => u.id == userid).FirstOrDefault();
                if (user == null)
                {
                    return new BadRequestObjectResult("No such user with given id");
                }
            } 
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }

            var shoppingList = new ShoppingList
            {
                DateCreated = DateTime.Now,
                User = user
            };
            try
            {
                using var context = new DatabaseContextFactory().CreateDbContext(null);
                context.Users.Attach(shoppingList.User);
                context.ShoppingLists.Add(shoppingList);
                context.SaveChanges();
                return new OkObjectResult(shoppingList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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
                shoppingList = context.ShoppingLists.Where(sl => sl.Id == dto.ShoppingListId).FirstOrDefault();
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
    }
}
