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
    public interface IOrderServiceFacade
    {
        public Task<Result> SendOrderAsync(OrderViewModel model);
        public Result<IQueryable<Order>> GetData();
        public Task<Result> RemoveAsync(Guid id);
        public Task<Result<OrderViewModel>> GetModelAsync(Guid id);
        public Result<IQueryable<OrderGridViewModel>> GetViewData();
        public Task<Result> ContactAsync(Guid id);
    }
}
