﻿using AutoMapper;
using Hookah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Areas.Administration.Models
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<PackageItemViewModel, PackageItem>().ReverseMap();
            CreateMap<PackageViewModel, Package>().ReverseMap();
            CreateMap<SignUpViewModel, User>().ReverseMap();
            CreateMap<PersonalInfoViewModel, User>().ReverseMap();
        }
    }
}
