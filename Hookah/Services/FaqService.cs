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
    public class FaqService : BaseService<Faq>, IFaqService
    {
        public FaqService(AppDbContext context):base(context)
        {

        }
        public async Task<Result<Faq>> GetDefaultDataAsync()
        {
            try
            {
                var data = await _context.Faqs.FirstOrDefaultAsync();
                if (data == null)
                {
                    return Result<Faq>.Failure(ExceptionMessages.NotFound);
                }
                return Result<Faq>.Succeed(data);
            }
            catch (ApplicationException ex)
            {
                return Result<Faq>.Failure(ex.Message);
            }
            catch (Exception e)
            {
                return Result<Faq>.Failure(e);
            }
        }
    }
}
