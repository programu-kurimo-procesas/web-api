using Microsoft.AspNetCore.Mvc;
using ScanAndGoApi.Context;
using ScanAndGoApi.Dtos;
using ScanAndGoApi.Models;

namespace ScanAndGoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController
    {
        
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpPost("CreateUser")]
        public IActionResult Create([FromBody] User user)
        {
            try
            {
                using var context = new DatabaseContextFactory().CreateDbContext(null);
                ShoppingList shoppingList = new ShoppingList();
                shoppingList.User = user;
                user.ShoppingList = shoppingList;
                context.Users.Attach(shoppingList.User);
                context.ShoppingLists.Add(shoppingList);
                context.Users.Add(user);
                context.SaveChanges();
                return new OkObjectResult(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpPost("GetUserByEmailAndPass")]
        public IActionResult GetUserByEmailAndPass([FromBody] UserLoginDto dto)
        {
            using (var context = new DatabaseContextFactory().CreateDbContext(null))
            {
                User? user = context.Users.Where(u => u.Email == dto.Email && u.Password == dto.Password).FirstOrDefault();
                return user != null ? new OkObjectResult(user) : new NotFoundResult();
            }
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                using (var context = new DatabaseContextFactory().CreateDbContext(null))
                {
                    List<User> users = context.Users.ToList();
                    return new OkObjectResult(users);
                }
            }
            catch (Exception e)
            {
                   return new BadRequestObjectResult(e.Message);
            }
        }
    }
}

