using Microsoft.AspNetCore.Mvc;

namespace PZ_Cheeseria.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheeseController : ControllerBase
    {

        [HttpGet(Name = "GetCheese")]
        public IEnumerable<Cheese> Get()
        {
            // Returns a list that contains 5 cheeses
            return new List<Cheese>()
            {
                new Cheese { Index = 1, Name = "Cheddar", Price = 29.36m, Colour = "Pale Yellow" },
                new Cheese { Index = 2, Name = "Feta", Price = 16.50m, Colour = "White" },
                new Cheese { Index = 3,  Name = "Mozzarella", Price = 25.99m, Colour = "White" },
                new Cheese { Index = 4,  Name = "Abbaye de Belloc", Price = 39.16m, Colour = "Yellow" },
                new Cheese { Index = 5,  Name = "Stilton", Price = 85.95m, Colour = "Blue-Grey" }
            };
        }
    }
}
