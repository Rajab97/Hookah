using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Abstracts
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<Result> CreateAsync(T model);
        Task<Result> EditAsync(T model);
        Task<Result> DeleteAsync(Guid id);
        Task<Result<T>> GetByIdAsync(Guid id);
        Result<IQueryable<T>> GetAll();

    }
}
