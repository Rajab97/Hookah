using Hookah.Abstracts;
using Hookah.Areas.Administration.Models;
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
    public class ContactService:BaseService<Contact>,IContactService
    {
        public ContactService(AppDbContext context):base(context)
        {

        }

        public async Task<Result<Contact>> GetDefaultDataAsync()
        {
            try
            {
                var data = await _context.Contacts.FirstOrDefaultAsync();
                if (data == null)
                {
                    return Result<Contact>.Failure(ExceptionMessages.NotFound);
                }
                return Result<Contact>.Succeed(data);
            }
            catch (ApplicationException ex)
            {
                return Result<Contact>.Failure(ex.Message);
            }
            catch (Exception e)
            {
                return Result<Contact>.Failure(e);
            }
        }
    }
}
