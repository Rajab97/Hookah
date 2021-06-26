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
    public class OrderService : BaseService<Order>, IOrderService
    {
        private readonly GeneralDbContext _generalDb;

        public OrderService(AppDbContext context,GeneralDbContext generalDb):base(context)
        {
            this._generalDb = generalDb;
        }
       

        public Result<IQueryable<OrderGridViewModel>> GetViewData()
        {
            try
            {
                var data = _generalDb.OrderGridView;
                return Result<IQueryable<OrderGridViewModel>>.Succeed(data);
            }
            catch (ApplicationException ex)
            {
                return Result<IQueryable<OrderGridViewModel>>.Failure(ex.Message);
            }
            catch (Exception e)
            {
                return Result<IQueryable<OrderGridViewModel>>.Failure(e);
            }
        }
    }
}
