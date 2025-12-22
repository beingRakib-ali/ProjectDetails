using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectDetails.Services;
using ProjectDetails.ViewModels;
using ProjectDetails.Helper;
using System.Data.Common;
using System.Collections.Generic;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace ProjectDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly PaymentService _paymentService;
        private readonly AppDBContext _context;
        private readonly IConfiguration _config;

        public PaymentController(PaymentService paymentService, AppDBContext context, IConfiguration config)
        {
            _paymentService = paymentService;
            _context = context;
            _config = config;
        }

        [HttpGet("debug/schema")]
        public IActionResult GetSchema()
        {
            try
            {
                var cs = _config.GetConnectionString("Postgres");
                using var conn = new NpgsqlConnection(cs);
                conn.Open();
                var result = new List<Dictionary<string, string>>();
                using var cmd = conn.CreateCommand();
                // list tables matching 'payment' case-insensitively to find actual table name
                cmd.CommandText = @"
SELECT tablename FROM pg_catalog.pg_tables WHERE schemaname='public' AND tablename ILIKE '%payment%';";
                using var tr = cmd.ExecuteReader();
                var tables = new List<string>();
                while (tr.Read()) { tables.Add(tr.GetString(0)); }
                tr.Close();

                if (tables.Count == 0) {
                    return Ok(new { tables = tables });
                }

                // inspect first matching table's columns
                var tname = tables[0];
                cmd.CommandText = $@"SELECT column_name, data_type, is_nullable FROM information_schema.columns WHERE table_name='{tname}' ORDER BY ordinal_position;";
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new() {
                        { "column", reader.GetString(0) },
                        { "type", reader.GetString(1) },
                        { "nullable", reader.GetString(2) }
                    });
                }
                conn.Close();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
