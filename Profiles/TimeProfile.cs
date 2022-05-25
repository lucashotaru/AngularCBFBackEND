using AutoMapper;
using AngularCBFBackEND.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularCBFBackEND.Data.Dtos.Time;
using AngularCBFBackEND.Data.Dtos;

namespace AngularCBFBackEND.Profiles
{
    public class TimeProfile : Profile
    {
        public TimeProfile()
        {
            CreateMap<CreateTimeDto, Time>();
            CreateMap<Time, ReadTimeDto>();
            CreateMap<UpdateTimeDto, Time>();
        }
    }
}
