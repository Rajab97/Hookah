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
    public class MenuService:BaseService<Menu>,IMenuService
    {
        public MenuService(AppDbContext context):base(context)
        {

        }

        public async Task<Result<Menu>> GetDefaultDataAsync()
        {
            try
            {
                var data = await _context.Menus.FirstOrDefaultAsync();
                if (data == null)
                {
                    return Result<Menu>.Failure(ExceptionMessages.NotFound);
                }
                return Result<Menu>.Succeed(data);
            }
            catch (ApplicationException ex)
            {
                return Result<Menu>.Failure(ex.Message);
            }
            catch (Exception e)
            {
                return Result<Menu>.Failure(e);
            }
            
        }
    }
}
