using Application.IServices;
using Domain.Models;
using Infrastructure.IRepository;

namespace Application.Services
{
    public class LoginService : IServices<Login>
    {
        private readonly IRepository<Login> _LoginRepository;
        public LoginService(IRepository<Login> LoginRepository)
        {
            _LoginRepository = LoginRepository;
        }
        public void Delete(Login entity)
        {
            try
            {
                if (entity != null)
                {
                    _LoginRepository.Delete(entity);
                    _LoginRepository.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Login Get(int Id)
        {
            try
            {
                var obj = _LoginRepository.Get(Id);
                if (obj != null)
                {
                    return obj;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Login> GetAll()
        {
            try
            {
                var obj = _LoginRepository.GetAll();
                if (obj != null)
                {
                    return obj;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Insert(Login entity)
        {
            try
            {
                if (entity != null)
                {
                    _LoginRepository.Insert(entity);
                    _LoginRepository.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Remove(Login entity)
        {
            try
            {
                if (entity != null)
                {
                    _LoginRepository.Remove(entity);
                    _LoginRepository.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Update(Login entity)
        {
            try
            {
                if (entity != null)
                {
                    _LoginRepository.Update(entity);
                    _LoginRepository.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
