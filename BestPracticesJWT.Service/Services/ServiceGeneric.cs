using BestPracticesJWT.Core.Interfaces.GenericRepository;
using BestPracticesJWT.Core.Interfaces.Services;
using BestPracticesJWT.Core.Interfaces.UnitOfWork;
using BestPracticesJWT.Service.Mappers;
using BestPracticesJWT.SharedCommons.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BestPracticesJWT.Service.Services;

public class ServiceGeneric<T, TDto> : IServiceGeneric<T, TDto> where T : class where TDto : class
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<T> _genericRepository;
    public ServiceGeneric(IUnitOfWork unitOfWork, IGenericRepository<T> genericRepository)
    {
        _unitOfWork = unitOfWork;
        _genericRepository = genericRepository;
    }

    public async Task<ResponseDto<TDto>> AddSync(TDto entity)
    {
        var newEntity = ObjectMapper.Mapper.Map<T>(entity);
        await _genericRepository.AddSync(newEntity);

        await _unitOfWork.CommitAsync();
        
        var newDto = ObjectMapper.Mapper.Map<TDto>(newEntity);
        return ResponseDto<TDto>.Success(newDto,200);

    }

    public async Task<ResponseDto<IEnumerable<TDto>>> GetAllAsync()
    {
        var products = ObjectMapper.Mapper.Map<List<TDto>>(await _genericRepository.GetAllAsync());
        return ResponseDto<IEnumerable<TDto>>.Success(products, 200);
    }

    public async Task<ResponseDto<TDto>> GetByIdAsync(Guid id)
    {
        var product = await _genericRepository.GetByIdAsync(id);
        if (product == null)
        {
            return ResponseDto<TDto>.Fail("Id not found", 404, true);
        }
        return ResponseDto<TDto>.Success(ObjectMapper.Mapper.Map<TDto>(product), 200);
    }

    public async Task<ResponseDto<IEnumerable<TDto>>> GetWhere(Expression<Func<T, bool>> expression)
    {
        var list = _genericRepository.GetWhere(expression);
        return ResponseDto<IEnumerable<TDto>>.Success(ObjectMapper.Mapper.Map<IEnumerable<TDto>>(await list.ToListAsync()), 200);
    }

    public async Task<ResponseDto<NoDataDto>> Remove(Guid id)
    {
        var isExistsEntity = await _genericRepository.GetByIdAsync(id);
        if (isExistsEntity == null)
        {
            return ResponseDto<NoDataDto>.Fail("Id not found", 404, true);
        }
        _genericRepository.Remove(isExistsEntity);
        await _unitOfWork.CommitAsync();
        return ResponseDto<NoDataDto>.Success(200);
    }

    public async Task<ResponseDto<NoDataDto>> Update(TDto entity, Guid id)
    {
        var isExitsEntity =  await _genericRepository.GetByIdAsync(id);
        if (isExitsEntity == null)
        {
            return ResponseDto<NoDataDto>.Fail("No data found", 404, true);
        }
        var updateEntity = ObjectMapper.Mapper.Map<T>(entity);
        _genericRepository.Update(updateEntity);
        await _unitOfWork.CommitAsync();
        return ResponseDto<NoDataDto>.Success(204);
    }
}
