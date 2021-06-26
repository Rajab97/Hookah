using AutoMapper;
using Hookah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.ViewModels
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<OrderViewModel, Order>().ForMember(m => m.EventDate, x => x.MapFrom(d => d.Day.HasValue && d.Year.HasValue && d.Month.HasValue && d.Time.HasValue ?
                                                                                    new DateTime(d.Year.Value, d.Month.Value, d.Day.Value, d.Time.Value.Hours, d.Time.Value.Minutes, d.Time.Value.Seconds) : DateTime.MinValue))
                                                .ReverseMap()
                                                            .ForMember(m=>m.Day,x=>x.MapFrom(t=>t.EventDate.HasValue ? t.EventDate.Value.Day : DateTime.Now.Day))
                                                                .ForMember(m => m.Month, x => x.MapFrom(t => t.EventDate.HasValue ? t.EventDate.Value.Month : DateTime.Now.Month))
                                                                    .ForMember(m => m.Year, x => x.MapFrom(t => t.EventDate.HasValue ? t.EventDate.Value.Year : DateTime.Now.Year))
                                                                        .ForMember(m => m.Time, x => x.MapFrom(t => t.EventDate.HasValue ? new TimeSpan(t.EventDate.Value.Hour, t.EventDate.Value.Minute, t.EventDate.Value.Second) : TimeSpan.Zero));
        }
    }
}
