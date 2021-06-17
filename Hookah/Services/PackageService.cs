using Hookah.Abstracts;
using Hookah.Data;
using Hookah.Exceptions;
using Hookah.Interfacas;
using Hookah.Models;
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
    }
}
