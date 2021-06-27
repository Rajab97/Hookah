using Hookah.Abstracts;
using Hookah.Areas.Administration.Models;
using Hookah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Interfacas
{
    public interface IFaqQuestionServiceFacade
    {
        public Task<Result> SaveAsync(FaqQuestionViewModel model);
        public Result<IQueryable<FaqQuestion>> GetData();
        public Task<Result> RemoveAsync(Guid id);
        public Task<Result<FaqQuestionViewModel>> GetModelAsync(Guid id);
    }
}
