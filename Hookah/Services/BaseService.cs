using Hookah.Abstracts;
using Hookah.Data;
using Hookah.Exceptions;
using Hookah.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Services
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity, new()
    {
        protected readonly AppDbContext _context;

        public BaseService(AppDbContext context)
        {
            _context = context;
        }
        public virtual async Task<Result> CreateAsync(T model)
        {
            try
            {
                model.AddedDate = DateTime.Now;
                await _context.AddAsync(model);
                return Result.Succeed();
            }
            catch (BaseException exc)
            {
                return Result.Failure(exc);
            }
            catch (Exception unknownExc)
            {
                var fatalExc = new FatalException(unknownExc);
                return Result.Failure(fatalExc);
            }
        }

        public virtual async Task<Result> EditAsync(T model)
        {
            try
            {
                if (await GetVersionOfOriginalEntity<T>(model.Id) != model.Version)
                {
                    throw new ConcurencyEditException(null);
                }
                model.UpdatedDate = DateTime.Now;
                model.Version++;
                _context.Entry(model).State = EntityState.Modified;
                return Result.Succeed();
            }
            catch (BaseException exc)
            {
                return Result.Failure(exc);
            }
            catch (Exception unknownExc)
            {
                var fatalExc = new FatalException(unknownExc);
                return Result.Failure(fatalExc);
            }
        }

        public virtual Result<IQueryable<T>> GetAll()
        {
            try
            {
                var result = _context.Set<T>();
                return Result<IQueryable<T>>.Succeed(result);
            }
            catch (BaseException exc)
            {
                return Result<IQueryable<T>>.Failure(exc);
            }
            catch (Exception unknownExc)
            {
                var fatalExc = new FatalException(unknownExc);
                return Result<IQueryable<T>>.Failure(fatalExc);
            }
        }

        public virtual async Task<Result<T>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _context.Set<T>().SingleOrDefaultAsync(m=>m.Id == id);
                if (result == null)
                    return Result<T>.Failure(ExceptionMessages.NotFound);

                return Result<T>.Succeed(result);
            }
            catch (BaseException exc)
            {
                return Result<T>.Failure(exc);
            }
            catch (Exception unknownExc)
            {
                var fatalExc = new FatalException(unknownExc);
                return Result<T>.Failure(fatalExc);
            }
        }

        public virtual async Task<Result> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                if (!entity.IsSucceed)
                {
                    return Result.Failure(entity.ExceptionMessage);
                }
                _context.Remove(entity.Data);
                return Result.Succeed();
            }
            catch (BaseException exc)
            {
                return Result.Failure(exc);
            }
            catch (Exception unknownExc)
            {
                var fatalExc = new FatalException(unknownExc);
                return Result.Failure(fatalExc);
            }
        }

        protected async Task<int> GetVersionOfOriginalEntity<T>(Guid id) where T : BaseEntity, new()
        {
            var version = await _context.Set<T>().Where(m => m.Id == id).Select(m => m.Version).SingleAsync();
            return version;
        }
    }
}
