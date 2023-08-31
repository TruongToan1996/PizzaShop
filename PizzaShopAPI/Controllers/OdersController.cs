using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Shared;
using PizzaShopAPI.Data;

namespace PizzaShopAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OdersController : ControllerBase
    {
       
        private readonly DbContextPizza? _db; //ket noi toi DataBase
        public OdersController(DbContextPizza? db)
        {
            _db = db;
        }


        [HttpGet]
        public async Task<IActionResult> GetOrders() // tra ve IActionReusult
        {
            // trong BaseRespone<> dinh nghia kieu gi thi DataResult se la kieu du lieu do
            BaseResponse<List<OrderWithStatus>> response = new BaseResponse<List<OrderWithStatus>>(); // khoi tao kieu response tra ve
            try
            {

                var orders = await _db.Orders// connect db
                    //.Where(o => o.UserId == GetUserId())
                    .Include(o => o.DeliveryLocation)// Gop truong du lieu
                    .Include(o => o.Pizzas).ThenInclude(p => p.Special) // lay truong Special trong pizzas chu khong lay het
                    .Include(o => o.Pizzas).ThenInclude(p => p.Toppings).ThenInclude(t => t.Topping)
                    .OrderByDescending(o => o.CreatedTime)// sap xep du lieu tra ve
                    .ToListAsync(); // ep du lieu thanh list

                response.DataResult = orders.Select(o => OrderWithStatus.FromOrder(o)).ToList();
                response.ResponseCode = 1;// set response code 1 -> thanh cong, -1 se la that bai
                response.Msg = "Success";// mo ta request
            }catch (Exception ex)
            {
                response.Msg = ex.Message; // gan mo ta la loi
                response.ResponseCode = -1;
            }
            return Ok(response);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderWithStatus(int orderId)
        {
            BaseResponse<OrderWithStatus> response = new BaseResponse<OrderWithStatus>();
            try
            {
                var order = await _db.Orders
               .Where(o => o.OrderId == orderId)
               .Where(o => o.UserId == GetUserId())
               .Include(o => o.DeliveryLocation)
               .Include(o => o.Pizzas).ThenInclude(p => p.Special)
               .Include(o => o.Pizzas).ThenInclude(p => p.Toppings).ThenInclude(t => t.Topping)
               .SingleOrDefaultAsync();
                response.DataResult = OrderWithStatus.FromOrder(order);
                response.Msg = "Success";
                response.ResponseCode = 1;
                    if (order == null)
                {
                    return NotFound();
                }
            }catch (Exception ex)
            {
                response.Msg = ex.Message;
                response.ResponseCode = -1;
            }
           

            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(Order order)
        {
            try
            {

                order.CreatedTime = DateTime.Now;
                order.DeliveryLocation = new LatLong(51.5001, -0.1239);
                order.UserId = GetUserId();

                // Enforce existence of Pizza.SpecialId and Topping.ToppingId
                // in the database - prevent the submitter from making up
                // new specials and toppings
                foreach (var pizza in order.Pizzas)
                {
                    pizza.SpecialId = pizza.Special.Id;
                    pizza.Special = null;

                    foreach (var topping in pizza.Toppings)
                    {
                        topping.ToppingId = topping.Topping.Id;
                        topping.Topping = null;
                    }
                }

                await _db.Orders.AddAsync(order);
                await _db.SaveChangesAsync();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return Ok(order);
        }

        private string GetUserId()
        {
            // This will be the user's twitter username
            return HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;
        }

    }
    
}
