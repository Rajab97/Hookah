using Hookah.Abstracts;
using Hookah.Areas.Administration.Models;
using Hookah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Interfacas
{
    public interface IPackageServiceFacade
    {
        public Task<Result> SaveAsync(PackageViewModel model);
        public Result<IQueryable<Package>> GetAll();
        public Result<IQueryable<PackageItem>> GetPackageItems(Guid packageId);
        public Result Edit(PackageItemViewModel packageItemViewModel);
        public Task<Result<PackageViewModel>> GetModelByIdAsync(Guid packageId);
    }
}
