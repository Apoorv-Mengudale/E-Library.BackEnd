using AutoMapper;
using Domain.Models.Books;
using Domain.Models.Users;

namespace Domain.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User -> AuthenticateResponse
            CreateMap<User, AuthenticateResponse>();

            // RegisterRequest -> User
            CreateMap<RegisterRequest, User>();

            // UpdateRequest -> User
            CreateMap<UpdateRequest, User>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        return prop is not string arg3 || !string.IsNullOrEmpty(arg3);
                    }
                ));

            CreateMap<UpdateBookRequest, Book>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        return prop is not string arg3 || !string.IsNullOrEmpty(arg3);
                    }
                ));
        }
    }
}
