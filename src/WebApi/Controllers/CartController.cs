using Microsoft.AspNetCore.Mvc;
using TechTrain.ReusableModules.WebApi.Models;

namespace TechTrain.ReusableModules.WebApi.Controllers
{
    [ApiController]
    public class CartController : ControllerBase
    {
        [HttpGet]
        [Route("/users/{userId}/cart")]
        public Task<Cart> GetCart([FromRoute] int userId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("/carts/{cartId}/items")]
        public Task<IEnumerable<CartItem>> ListCartItems([FromRoute] string cartId)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("/carts/{cartId}/items")]
        public Task<IActionResult> AddItemToCart([FromRoute] string cartId, [FromBody] CartItem item)
        {
            throw new NotImplementedException();
        }
    }
}
