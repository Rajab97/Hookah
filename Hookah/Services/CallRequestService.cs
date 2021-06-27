using Hookah.Abstracts;
using Hookah.Data;
using Hookah.Interfacas;
using Hookah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Services
{
    public class CallRequestService : BaseService<CallRequest>, ICallRequestService
    {
        private readonly GeneralDbContext _generalDb;

        public CallRequestService(AppDbContext context,GeneralDbContext generalDb):base(context)
        {
            this._generalDb = generalDb;
        }
       

        public Result<IQueryable<CallRequestGridViewModel>> GetViewData()
        {
            try
            {
                var data = _generalDb.CallRequestGridView;
                return Result<IQueryable<CallRequestGridViewModel>>.Succeed(data);
            }
            catch (ApplicationException ex)
            {
                return Result<IQueryable<CallRequestGridViewModel>>.Failure(ex.Message);
            }
            catch (Exception e)
            {
                return Result<IQueryable<CallRequestGridViewModel>>.Failure(e);
            }
        }
    }
}
