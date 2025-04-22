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

        }


    }
}
