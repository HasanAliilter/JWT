using SharedLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthServer.Core.Services
{
    public interface IBaseService<T, TDto> where T : class where TDto : class
    {
        Task <Response<TDto>> GetByIdAsync(int id);
        Response<TDto> GetById(int id);
        Task<Response<IEnumerable<TDto>>> GetAllAsync();
        Response<IEnumerable<TDto>> GetAll();
        Task<Response<IEnumerable<TDto>>> Where(Expression<Func<T, bool>> predicate); //api üzerinde artık herhangi bir bussiness kodu yazmayacağımız için IEnumerable doğru seçim olacaktır
        Task<Response<TDto>> AddAsync(TDto entity);
        Task<Response<EmptyDto>> Update(int id, TDto entity);
        Task<Response<EmptyDto>> Remove(int id);
    }
}
