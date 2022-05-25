using AutoMapper;
using AngularCBFBackEND.Data.Dtos.Temporada;
using AngularCBFBackEND.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularCBFBackEND.Profiles
{
    public class TemporadaProfile : Profile
    {
        public TemporadaProfile()
        {
            CreateMap<CreateTemporadaDto, Temporada>();
            CreateMap<Temporada, ReadTemporadaDto>()
                .ForMember(temporada => temporada.Times, opts => opts
                .MapFrom(temporada => temporada.Times.Select
                (t => new { t.Id, t.Nome, t.Ataque, t.MeioCampo, t.Defesa })));
            CreateMap<UpdateTemporadaDto, Temporada>();
        }
    }
}
