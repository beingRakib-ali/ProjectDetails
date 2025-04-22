using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectDetails.Helper;
using ProjectDetails.Models;
using ProjectDetails.ViewModels;

namespace ProjectDetails.Services
{
    public class PaymentService
    {

        private readonly AppDBContext _context;
        private readonly IMapper _mapper;
        public PaymentService(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Payment_ViewModels>> GetAllPayments()
        {
            var payments = await _context.paymentDetails.ToListAsync();
            if (payments == null)
            {
                return null;
            }
            var data = _mapper.Map<List<Payment_ViewModels>>(payments);
            return data;
        }

        public async Task<Payment_ViewModels> GetPaymentById(int id)
        {
            var payment = await _context.paymentDetails.FindAsync(id);
            if (payment == null)
            {
                return null;
            }
            var data = _mapper.Map<Payment_ViewModels>(payment);
            return data;
        }

        public async Task<Payment_ViewModels> CreatePayment(Payment_ViewModels payment)
        {
            if (payment == null)
            {
                return null;
            }

            var data = _mapper.Map<paymentDetails>(payment);
            await _context.paymentDetails.AddAsync(data);
            var result = await _context.SaveChangesAsync();
            return await GetPaymentById(data.Id);
        }

        public async Task<Payment_ViewModels> UpdatePayment(int id, Payment_ViewModels payment)
        {
            if (payment == null)
            {
                return null;
            }
            var existingPayment = await _context.paymentDetails.FindAsync(id);
            if (existingPayment == null)
            {
                return null;
            }
            var data = _mapper.Map<paymentDetails>(payment);
            _context.Entry(existingPayment).CurrentValues.SetValues(data);
            await _context.SaveChangesAsync();
            return await GetPaymentById(id);
        }


        public async Task<bool> DeletePayment(int id)
        {
            var payment = await _context.paymentDetails.FindAsync(id);
            if (payment == null)
            {
                return false;
            }
            payment.Status = 255;
            _context.paymentDetails.Update(payment);
            await _context.SaveChangesAsync();
            return true;
        }












    }
}
