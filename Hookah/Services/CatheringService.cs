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
    public class CatheringService:BaseService<Cathering>,ICatheringService
    {
        public CatheringService(AppDbContext context):base(context)
        {

        }

        public async Task<Result<Cathering>> GetDefaultDataAsync()
        {
            try
            {
                var data = await _context.Cathering.FirstOrDefaultAsync();
                if (data == null)
                {
                    return Result<Cathering>.Failure(ExceptionMessages.NotFound);
                }
                return Result<Cathering>.Succeed(data);
            }
            catch (ApplicationException ex)
            {
                return Result<Cathering>.Failure(ex.Message);
            }
            catch (Exception e)
            {
                return Result<Cathering>.Failure(e);
            }
            
        }
    }
}
