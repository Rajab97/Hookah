using Hookah.Abstracts;
using Hookah.Areas.Administration.Models;
using Hookah.Models;
using Hookah.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Interfacas
{
    public interface ICallRequestServiceFacade
    {
        public Task<Result> SendCallRequestAsync(CallRequestViewModel model);
        public Result<IQueryable<CallRequest>> GetData();
        public Task<Result> RemoveAsync(Guid id);
        public Task<Result<CallRequestViewModel>> GetModelAsync(Guid id);
        public Result<IQueryable<CallRequestGridViewModel>> GetViewData();
        public Task<Result> ContactAsync(Guid id);
    }
}
