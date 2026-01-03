using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectDetails.Helper;
using ProjectDetails.ViewModels;

namespace ProjectDetails.Services
{
    public class OrderService
    {
        private readonly AppDBContext _bd;
        private readonly IMapper _mapper;
        public OrderService(AppDBContext db, IMapper mapper)
        {
            _bd = db;
            _mapper = mapper;
        }


        public async Task<List<Order_ViewModel>> GetAllOrders()
        {
            var data = await _bd.Order_Tbl.Where(a => a.StatusID != 255).ToListAsync();
            if (data == null)
            {
                return null;
            }
            var result = _mapper.Map<List<Order_ViewModel>>(data);
            return result;

        }

        public async Task<Order_ViewModel> GetOrderById(int OrderID)
        {
            var order = await _bd.Order_Tbl.Where(a => a.StatusID != 255 && a.OrderID == OrderID).FirstOrDefaultAsync();
            if (order == null)
            {
                return null;
            }
            var data = _mapper.Map<Order_ViewModel>(order);
            return data;
        }


        public async Task<Order_ViewModel> CreateOrder(Order_ViewModel order)
        {
            if (order == null)
            {
                return null;
            }
            var data = _mapper.Map<Models.Order_Tbl>(order);
            await _bd.Order_Tbl.AddAsync(data);
            await _bd.SaveChangesAsync();
            var result = _mapper.Map<Order_ViewModel>(data);
            return result;
        }

        public async Task<Order_ViewModel> UpdateOrder(int OrderID, Order_ViewModel order)
        {
            var existingOrder = await _bd.Order_Tbl.FindAsync(OrderID);
            if (existingOrder == null)
            {
                return null;
            }
            _mapper.Map(order, existingOrder);
            _bd.Order_Tbl.Update(existingOrder);
            await _bd.SaveChangesAsync();
            var result = _mapper.Map<Order_ViewModel>(existingOrder);
            return result;
        }

        public async Task<bool> DeleteOrder(int OrderID)
        {
            var existingOrder = await _bd.Order_Tbl.FindAsync(OrderID);
            if (existingOrder == null)
            {
                return false;
            }
            existingOrder.StatusID = 255;
            _bd.Order_Tbl.Update(existingOrder);
            await _bd.SaveChangesAsync();
            return true;
        }

        public async Task<List<Order_ViewModel>> Get_OrderByCategoryProduct(int ProductID,int CategoryID,int CmdID)
        {
            

            if(CmdID == 0)
            {
                var orders = await _bd.Order_Tbl
                .Where(o => o.ProductID == ProductID && o.StatusID != 255)
                .ToListAsync();
                var result = _mapper.Map<List<Order_ViewModel>>(orders);
                return result;
            }


            else
            {
                var data = await (
            from c in _bd.Category_Tbl.Where(a => a.StatusId != 255)
            join p in _bd.Product_Tbl.Where(a=>a.StatusID != 255) on c.CategoryId equals p.CategoryId
            join o in _bd.Order_Tbl.Where(a => a.StatusID != 255) on p.ProductId equals o.ProductID
            where c.CategoryId == CategoryID
            select new Order_ViewModel
            {
                OrderID = o.OrderID,
                ProductID = o.ProductID,
                Name = o.Name,
                Email = o.Email,
                PhoneNumber = o.PhoneNumber,
                Address = o.Address,
                City = o.City,
                Country = o.Country,
                ZipCode = o.ZipCode,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                OrderStatus = o.OrderStatus,
                CreatedDate = o.CreatedDate,
                CreatedBy = o.CreatedBy,
                StatusID = o.StatusID
            }
            ).ToListAsync();
                return data;
            }
           
        }


        public async Task<List<Order_ViewModel>> OrdersByCategory(int CategoryID)
        {
            var data = await (
            from c in _bd.Category_Tbl
            join p in _bd.Product_Tbl on c.CategoryId equals p.CategoryId
            join o in _bd.Order_Tbl on p.ProductId equals o.ProductID
            where c.StatusId != 255
            && p.StatusID != 255
            && o.StatusID != 255
            && c.CategoryId == CategoryID
            select new Order_ViewModel
            {
                OrderID = o.OrderID,
                ProductID = o.ProductID,
                Name = o.Name,
                Email = o.Email,
                PhoneNumber = o.PhoneNumber,
                Address = o.Address,
                City = o.City,
                Country = o.Country,
                ZipCode = o.ZipCode,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                OrderStatus = o.OrderStatus,
                CreatedDate = o.CreatedDate,
                CreatedBy = o.CreatedBy,
                StatusID = o.StatusID
            }
            ).ToListAsync();
            return data;
        }









    }
}
