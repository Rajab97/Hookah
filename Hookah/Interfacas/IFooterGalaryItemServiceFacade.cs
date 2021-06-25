using Hookah.Abstracts;
using Hookah.Areas.Administration.Models;
using Hookah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Interfacas
{
    public interface IFooterGalaryItemServiceFacade
    {
        public Task<Result> SaveAsync(FooterGalaryItemViewModel model);
        public Result<IQueryable<FooterGalaryItem>> GetData();
        public Task<Result> RemoveAsync(Guid id);
        public Task<Result<FooterGalaryItemViewModel>> GetModelAsync(Guid id);
    }
}
