using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using AutoMapper;

namespace AddressValidator.Data.Models.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Address, ValidatedAddress>()
                /* ignore */
                .ForMember(m => m.Business, o => o.Ignore())
                .ForMember(m => m.Latitude, o => o.Ignore())
                .ForMember(m => m.Longitude, o => o.Ignore())
                .ForMember(m => m.Valid, o => o.Ignore())
                .ForMember(m => m.UiMessage, o => o.Ignore());

            CreateMap<AddressValidatorRequest, AddressValidatorResult>()
                /* addresses */
                .ForMember(m => m.Addresses, o => o.MapFrom(s => s.Addresses))
                /* address validator */
                .ForMember(m => m.AddressValidatorService,
                    opt => 
                        opt.MapFrom((src, dst, _, context) => 
                            context.Options.Items["AddressValidatorService"]
                        )
                );
        }
    }
}
