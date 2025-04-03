using AutoMapper;
using LeadManagement.Application.DTOs;
using LeadManagement.Domain.Entities;

namespace LeadManagement.Application.Mappings
{
    public class LeadProfile : Profile
    {
        public LeadProfile()
        {
            CreateMap<Lead, LeadDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
