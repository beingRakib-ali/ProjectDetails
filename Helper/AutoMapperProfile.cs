using AutoMapper;
using ProjectDetails.Models;
using ProjectDetails.ViewModels;


namespace ProjectDetails.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<paymentDetails, Payment_ViewModels>().ReverseMap();
            CreateMap<Category_Tbl, Category_ViewModel>().ReverseMap();
            CreateMap<Blogs_Tbl,Blogs_ViewModels>().ReverseMap();
            CreateMap<Order_Tbl,Order_ViewModel>().ReverseMap();
            CreateMap<Product_Tbl,ProductCreate_VM>().ReverseMap();
            //CreateMap<ProductImage_Tbl,ProductImage_VM>().ReverseMap();
        }


    }
}
