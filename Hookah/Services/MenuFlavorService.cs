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
    public class MenuFlavorService : BaseService<MenuFlavor>, IMenuFlavorService
    {
        public MenuFlavorService(AppDbContext context):base(context)
        {

        }
       
    }
}
