using Domain.Models;
using Infrastructure.IRepository;

namespace Application.Services
{
    public class LoginServiceBase
    {
        private readonly IRepository<Login> _LoginRepository;
    }
}