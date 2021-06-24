using Hookah.Abstracts;
using Hookah.Data;
using Hookah.Interfacas;
using Hookah.Models;
using Hookah.Resources;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Services
{
    public class HomeLinkService : BaseService<HomeLink> , IHomeLinkService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeLinkService(AppDbContext context,IWebHostEnvironment webHostEnvironment) : base(context)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public override async Task<Result> DeleteAsync(Guid id)
        {
            try
            {
                var data = await _context.HomeLinks.FirstOrDefaultAsync(m => m.Id == id);

                if (data == null)
                    return Result.Failure(ExceptionMessages.NotFound);

                var path = Path.Combine(_webHostEnvironment.WebRootPath, data.ImagePath);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                return await base.DeleteAsync(id);
            }
            catch (ApplicationException ex)
            {
                return Result.Failure(ex.Message);
            }
            catch(Exception e)
            {
                return Result.Failure(e);
            }
        }
    }
}
