using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectDetails.Services;
using ProjectDetails.ViewModels;

namespace ProjectDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _service;
        private readonly IMapper _mapper;
        public OrderController(OrderService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }



        [HttpGet("GetAllOrders")]
        public async Task<ActionResult<List<Order_ViewModel>>> Get()
        {
            var data = await _service.GetAllOrders(); // ✅ await
            return Ok(data);
        }

        [HttpGet("GetOrderById/{OrderID}")]
        public async Task<ActionResult<Order_ViewModel>> Get(int id)
        {
            var data = await _service.GetOrderById(id); // ✅ await
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpPost("CreateOrder")]
        public async Task<ActionResult<Order_ViewModel>> Post(Order_ViewModel order)
        {
            if (order == null)
                return BadRequest("Order cannot be null");

            var data = await _service.CreateOrder(order); // ✅ await here
            return Ok(data);
        }


        [HttpPut("UpdateOrder/{OrderID}")]
        public async Task<ActionResult<Order_ViewModel>> Put(int id, Order_ViewModel order)
        {
            var data = await _service.UpdateOrder(id, order); // ✅ await
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpPut("DeleteOrderById/{OrderID}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var success = await _service.DeleteOrder(id); // ✅ await
            return Ok(success);
        }

        [HttpGet("Get_OrderByCategoryProduct")]
        public async Task<ActionResult<List<Order_ViewModel>>> Get_OrderByCategoryProduct(int CategoryID,int ProductID,int CmdID)
        {
            var data = await _service.Get_OrderByCategoryProduct(CategoryID ,ProductID,CmdID); 
            return Ok(data);
        }

        [HttpGet("Get_OrderByCategory")]
        public async Task<ActionResult<List<Order_ViewModel>>> OrdersByCategory(int CategoryID)
        {
            var data = await _service.OrdersByCategory(CategoryID);
            return Ok(data);
        }
    }
}
