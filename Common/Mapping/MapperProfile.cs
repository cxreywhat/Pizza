namespace Pizza.Mapping.Common
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PizzaModel, CreatePizza>().ReverseMap();
            CreateMap<PizzaModel, UpdatePizza>().ReverseMap();
            CreateMap<UserModel, CreateUser>().ReverseMap();
            CreateMap<UserModel, UpdateUser>().ReverseMap();
            CreateMap<UserModel, UserResponse>().ReverseMap();
            CreateMap<UserModel, AuthRegister>().ReverseMap();
            CreateMap<UserModel, AuthLogin>().ReverseMap();
        }
    }
}