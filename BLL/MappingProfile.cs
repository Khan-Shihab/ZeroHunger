using AutoMapper;
using BLL.Models;
using DAL.EF.Tables;

namespace BLL
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User,AuthModel>().ReverseMap();
            CreateMap<User, UserUpdateModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();

            CreateMap<Restaurant, AuthModel>().ReverseMap();
            CreateMap<Restaurant,RestaurantModel>().ReverseMap();
            CreateMap<Restaurant,RestaurantUpdateModel>().ReverseMap();

            CreateMap<Employee,EmployeeUpdateModel>().ReverseMap();
            CreateMap<Employee, EmployeeModel>().ReverseMap();
            CreateMap<Employee, AuthModel>().ReverseMap();

            CreateMap<CollectRequestModel, CollectRequest>().ReverseMap();

        }

    }
}
