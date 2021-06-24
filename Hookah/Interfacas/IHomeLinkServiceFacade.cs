using Hookah.Abstracts;
using Hookah.Areas.Administration.Models;
using Hookah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Interfacas
{
    public interface IHomeLinkServiceFacade
    {
        public Task<Result> SaveAsync(HomeLinkViewModel model);
        public Result<IQueryable<HomeLink>> GetData();
        public Task<Result> RemoveAsync(Guid id);
        public Task<Result<HomeLinkViewModel>> GetModelAsync(Guid id);
    }
}
