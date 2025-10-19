using BestPracticesJWT.SharedCommons.Dtos;
using System.Linq.Expressions;

namespace BestPracticesJWT.Core.Interfaces.Services;

public interface IServiceGeneric<T, TDto> where T : class where TDto : class
{
    Task<ResponseDto<TDto>> GetByIdAsync(Guid id);
    Task<ResponseDto<IEnumerable<TDto>>> GetAllAsync();
    Task<ResponseDto<TDto>> AddSync(T entity);
    Task<ResponseDto<IEnumerable<TDto>>> GetWhere(Expression<Func<ResponseDto<T>, bool>> expression);
    Task<ResponseDto<NoDataDto>> Remove(T entity);
    Task<ResponseDto<NoDataDto>> Update(T entity);
}
