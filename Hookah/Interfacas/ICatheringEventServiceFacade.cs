using Hookah.Abstracts;
using Hookah.Areas.Administration.Models;
using Hookah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Interfacas
{
    public interface ICatheringEventServiceFacade
    {
        public Task<Result> SaveAsync(CatheringEventViewModel model);
        public Result<IQueryable<CatheringEvent>> GetData();
        public Task<Result> RemoveAsync(Guid id);
        public Task<Result<CatheringEventViewModel>> GetModelAsync(Guid id);
    }
}
