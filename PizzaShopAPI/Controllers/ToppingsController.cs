
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Shared;
using PizzaShopAPI.Data;

namespace PizzaShopAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ToppingsController : ControllerBase
    {
        private readonly DbContextPizza _db;
        public ToppingsController(DbContextPizza db)
        {
            _db = db;
        }



        [HttpGet]
        public async Task<IActionResult> GetToppings()
        {

            BaseResponse<List<Topping>> response = new BaseResponse<List<Topping>>();
            try
            {
                var result = await _db.Toppings.OrderBy(t => t.Name).ToListAsync();
                response.DataResult = result;
                response.ResponseCode = 1;
                response.Msg = "Success";
            }
            catch (Exception ex)
            {
                response.DataResult = new();
                response.Msg = ex.Message;
                response.ResponseCode = -1;
            }
            return Ok(response);
          
        }




    }
}

