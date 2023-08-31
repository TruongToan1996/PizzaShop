using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Shared;
using PizzaShopAPI.Data;

namespace PizzaShopAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SpecialsController : ControllerBase
    {
        private readonly DbContextPizza dbContextPizza;

        public SpecialsController(DbContextPizza dbContextPizza)
        {
            this.dbContextPizza = dbContextPizza;
        }
        [HttpGet("specials")]
        public async Task<IActionResult> GetList()
        {
            BaseResponse<List<PizzaSpecial>> response = new BaseResponse<List<PizzaSpecial>>();
            try
            {
                var pizzas = await dbContextPizza.Specials.ToListAsync();
                response.DataResult = pizzas;
                response.ResponseCode = 1;
                response.Msg = "Success";
            }catch (Exception ex)
            {
                response.DataResult = new();
                response.Msg = ex.Message;
                response.ResponseCode = -1;
            }
            return Ok(response);
        }
    }
}
