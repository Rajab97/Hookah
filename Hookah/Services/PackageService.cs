using Hookah.Abstracts;
using Hookah.Data;
using Hookah.Exceptions;
using Hookah.Interfacas;
using Hookah.Models;
using Hookah.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Services
{
    public class PackageService:BaseService<Package>,IPackageService
    {
        private readonly IBaseService<PackageItem> _packageItemService;

        public PackageService(AppDbContext context,IBaseService<PackageItem> packageItemService):base(context)
        {
            this._packageItemService = packageItemService;
        }

        public override async Task<Result> CreateAsync(Package model)
        {
            try
            {
                var result = await base.CreateAsync(model);
                if (result.IsSucceed)
                {
                    return Result.Succeed();
                }
                return Result.Failure(result.Exception);
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
     /*   public override async Task<Result> EditAsync(Package model)
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
        }*/

        public override async Task<Result<Package>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _context.Packages.Include(m=>m.Items).SingleOrDefaultAsync(m => m.Id == id);
                if (result == null)
                    return Result<Package>.Failure(ExceptionMessages.NotFound);

                return Result<Package>.Succeed(result);
            }
            catch (BaseException exc)
            {
                return Result<Package>.Failure(exc);
            }
            catch (Exception unknownExc)
            {
                var fatalExc = new FatalException(unknownExc);
                return Result<Package>.Failure(fatalExc);
            }
        }
        public Result<IQueryable<PackageItem>> GetPackageItems(Guid packageId)
        {
            try
            {
                var result = _context.Set<PackageItem>().Where(m=>m.PackageId == packageId);
                return Result<IQueryable<PackageItem>>.Succeed(result);
            }
            catch (BaseException exc)
            {
                return Result<IQueryable<PackageItem>>.Failure(exc);
            }
            catch (Exception unknownExc)
            {
                var fatalExc = new FatalException(unknownExc);
                return Result<IQueryable<PackageItem>>.Failure(fatalExc);
            }
        }
    }
}
