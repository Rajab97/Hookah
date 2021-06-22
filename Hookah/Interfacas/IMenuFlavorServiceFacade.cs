using Hookah.Abstracts;
using Hookah.Areas.Administration.Models;
using Hookah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Interfacas
{
    public interface IMenuFlavorServiceFacade
    {
        public Task<Result> SaveAsync(MenuFlavorViewModel model);
        public Result<IQueryable<MenuFlavor>> GetData();
        public Task<Result> RemoveAsync(Guid id);
        public Task<Result<MenuFlavorViewModel>> GetModelAsync(Guid id);
    }
}
