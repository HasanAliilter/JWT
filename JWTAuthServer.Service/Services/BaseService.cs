using JWTAuthServer.Core.Interfaces;
using JWTAuthServer.Core.Models;
using JWTAuthServer.Core.Services;
using JWTAuthServer.Core.UnitOfWork;
using JWTAuthServer.Service.AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthServer.Service.Services
{
    public class BaseService<TEntity, TDto> : IBaseService<TEntity, TDto> where TDto : class where TEntity : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TEntity> _genericRepository;

        public BaseService(IUnitOfWork unitOfWork, IGenericRepository<TEntity> genericRepository)
        {
            _unitOfWork = unitOfWork;
            _genericRepository = genericRepository;
        }

        public async Task<Response<TDto>> AddAsync(TDto entity)
        {
            var mappedEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
            await _genericRepository.AddAsync(mappedEntity);
            await _unitOfWork.CommitAsync();
            var newDto = ObjectMapper.Mapper.Map<TDto>(mappedEntity);
            return Response<TDto>.Success(newDto, 200);
        }

        public Response<IEnumerable<TDto>> GetAll()
        {
            var datas = ObjectMapper.Mapper.Map<List<TDto>>(_genericRepository.GetAll());
            return Response<IEnumerable<TDto>>.Success(datas, 200);
        }

        public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
        {
            var datas = ObjectMapper.Mapper.Map<List<TDto>>(await _genericRepository.GetAllAsync());
            return Response<IEnumerable<TDto>>.Success(datas, 200);
        }

        public Response<TDto> GetById(int id)
        {
            var data = _genericRepository.GetById(id);

            if (data == null)
            {
                return Response<TDto>.Fail("Id not found", 404, true);
            }

            return Response<TDto>.Success(ObjectMapper.Mapper.Map<TDto>(data), 200);
        }

        public async Task<Response<TDto>> GetByIdAsync(int id)
        {
            var data = await _genericRepository.GetByIdAsync(id);

            if (data == null)
            {
                return Response<TDto>.Fail("Id not found", 404, true);
            }

            return Response<TDto>.Success(ObjectMapper.Mapper.Map<TDto>(data), 200);
        }

        public async Task<Response<EmptyDto>> Remove(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return Response<EmptyDto>.Fail("Id not found", 404, true);
            }

            _genericRepository.Remove(entity);
            await _unitOfWork.CommitAsync();
            return Response<EmptyDto>.Success(204);
        }

        public async Task<Response<EmptyDto>> Update(int id, TDto entity)
        {
            var foundEntity = await _genericRepository.GetByIdAsync(id);
            if (foundEntity == null)
            {
                return Response<EmptyDto>.Fail("Id not found", 404, true);
            }
            var mappedEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
            _genericRepository.Update(mappedEntity);
            await _unitOfWork.CommitAsync();
            return Response<EmptyDto>.Success(204);

        }

        public async Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var list = _genericRepository.Where(predicate);
            return Response<IEnumerable<TDto>>.Success(ObjectMapper.Mapper.Map<IEnumerable<TDto>>(await list.ToListAsync()), 200);
        }
    }
}
