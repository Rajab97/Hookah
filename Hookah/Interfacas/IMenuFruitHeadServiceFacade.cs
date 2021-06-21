using Hookah.Abstracts;
using Hookah.Areas.Administration.Models;
using Hookah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Interfacas
{
    public interface IMenuFruitHeadServiceFacade
    {
        public Task<Result> SaveAsync(MenuFruitHeadViewModel model);
        public Result<IQueryable<MenuFruitHead>> GetData();
        public Task<Result> RemoveAsync(Guid id);
        public Task<Result<MenuFruitHeadViewModel>> GetModelAsync(Guid id);
    }
}
