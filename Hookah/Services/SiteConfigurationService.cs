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
    public class SiteConfigurationService : BaseService<SiteConfiguration>, ISiteConfigurationService
    {
        public SiteConfigurationService(AppDbContext context):base(context)
        {

        }

        public async Task<Result<SiteConfiguration>> GetDefaultDataAsync()
        {
            try
            {
                var data = await _context.SiteConfiguration.FirstOrDefaultAsync();
                if (data == null)
                {
                    return Result<SiteConfiguration>.Failure(ExceptionMessages.NotFound);
                }
                return Result<SiteConfiguration>.Succeed(data);
            }
            catch (ApplicationException ex)
            {
                return Result<SiteConfiguration>.Failure(ex.Message);
            }
            catch (Exception e)
            {
                return Result<SiteConfiguration>.Failure(e);
            }
            
        }
    }
}
