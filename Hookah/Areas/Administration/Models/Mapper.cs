using AutoMapper;
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
            CreateMap<MenuFruitHeadViewModel, MenuFruitHead>().ReverseMap();
            CreateMap<MenuViewModel, Menu>().ReverseMap();
            CreateMap<MenuFlavorViewModel, MenuFlavor>().ReverseMap();
            CreateMap<HomeViewModel, Home>().ReverseMap();
            CreateMap<HomeLinkViewModel, HomeLink>().ReverseMap();
            CreateMap<CatheringViewModel, Cathering>().ReverseMap();
            CreateMap<CatheringEventViewModel, CatheringEvent>().ReverseMap();
            CreateMap<HowItWorksStepViewModel, HowItWorksStep>().ReverseMap();
            CreateMap<ContactViewModel, Contact>().ReverseMap();

            CreateMap<FooterGalaryItemViewModel, FooterGalaryItem>().ReverseMap();
            CreateMap<SiteConfigurationViewModel, SiteConfiguration>().ReverseMap();
        }
    }
}
