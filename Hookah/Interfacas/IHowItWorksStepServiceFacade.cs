using Hookah.Abstracts;
using Hookah.Areas.Administration.Models;
using Hookah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Interfacas
{
    public interface IHowItWorksStepServiceFacade
    {
        public Task<Result> SaveAsync(HowItWorksStepViewModel model);
        public Result<IQueryable<HowItWorksStep>> GetData();
        public Task<Result> RemoveAsync(Guid id);
        public Task<Result<HowItWorksStepViewModel>> GetModelAsync(Guid id);
    }
}
