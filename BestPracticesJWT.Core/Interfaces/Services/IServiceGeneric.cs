using BestPracticesJWT.SharedCommons.Dtos;
using System.Linq.Expressions;

namespace BestPracticesJWT.Core.Interfaces.Services;

public interface IServiceGeneric<T, TDto> where T : class where TDto : class
{
    Task<ResponseDto<TDto>> GetByIdAsync(Guid id);
    Task<ResponseDto<IEnumerable<TDto>>> GetAllAsync();
    Task<ResponseDto<TDto>> AddSync(TDto entity);
    Task<ResponseDto<IEnumerable<TDto>>> GetWhere(Expression<Func<T, bool>> expression);
    Task<ResponseDto<NoDataDto>> Remove(Guid id);
    Task<ResponseDto<NoDataDto>> Update(TDto entity, Guid id);
}
