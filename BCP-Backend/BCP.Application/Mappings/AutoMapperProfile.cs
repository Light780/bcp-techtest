using AutoMapper;
using BCP.Application.DTOs.Moneda;
using BCP.Application.DTOs.TipoCambio;
using BCP.Application.DTOs.Usuario;
using BCP.Domain.Entities;

namespace BCP.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Moneda, GetMonedaResponse>()
                .ForMember(p => p.Id, opt => opt.MapFrom(p => p.Id.ToString()));


            CreateMap<CreateTipoCambioRequest, TipoCambio>()
                .ForMember(p => p.Moneda, opt => opt.Ignore())
                .ForMember(p => p.IdMoneda, opt => opt.Ignore())
                .ForMember(p => p.Id, opt => opt.Ignore());
            
            CreateMap<TipoCambio, GetTipoCambioResponse>()
                .ForMember(p => p.Id, opt => opt.MapFrom(p => p.Id.ToString()))
                .ForMember(p => p.Fecha, opt => opt.MapFrom(p => p.Fecha.ToString("dd/MM/yyyy")))
                .ForMember(p => p.Moneda,
                    opt => opt.MapFrom(p => string.Concat(p.Moneda.CodigoSunat, $" ({p.Moneda.Nombre})")));

            CreateMap<RegisterUsuarioRequest, Usuario>()
                .ForMember(p => p.Id, opt => opt.Ignore());
        }
    }
}