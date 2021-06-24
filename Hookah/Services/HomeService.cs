using Hookah.Abstracts;
using Hookah.Data;
using Hookah.Interfacas;
using Hookah.Models;
using Hookah.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Services
{
    public class HomeService:BaseService<Home>,IHomeService
    {
        public HomeService(AppDbContext context):base(context)
        {

        }

        public async Task<Result<Home>> GetDefaultDataAsync()
        {
            try
            {
                var data = await _context.Home.FirstOrDefaultAsync();
                if (data == null)
                {
                    return Result<Home>.Failure(ExceptionMessages.NotFound);
                }
                return Result<Home>.Succeed(data);
            }
            catch (ApplicationException ex)
            {
                return Result<Home>.Failure(ex.Message);
            }
            catch (Exception e)
            {
                return Result<Home>.Failure(e);
            }
            
        }
    }
}
