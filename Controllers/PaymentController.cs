using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectDetails.Services;
using ProjectDetails.ViewModels;

namespace ProjectDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly PaymentService _paymentService;


        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        [HttpGet]
        public async Task<ActionResult<List<Payment_ViewModels>>> GetAllPayments()
        {
            var payments = await _paymentService.GetAllPayments();
            if (payments == null)
            {
                return NotFound();
            }
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Payment_ViewModels>> GetPaymentById(int id)
        {
            var payment = await _paymentService.GetPaymentById(id);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        [HttpPost]
        public async Task<ActionResult<Payment_ViewModels>> CreatePayment([FromBody] Payment_ViewModels payment)
        {
            if (payment == null)
            {
                return BadRequest();
            }
            var result = await _paymentService.CreatePayment(payment);
            if (result == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetPaymentById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Payment_ViewModels>> UpdatePayment(int id,Payment_ViewModels payment)
        {
            if (payment == null)
            {
                return BadRequest();
            }
            var result = await _paymentService.UpdatePayment(id, payment);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeletePayment(int id)
        {
            var result = await _paymentService.DeletePayment(id);
            if (result == false)
            {
                return false;
            }
            return true;
        }




    }
}
