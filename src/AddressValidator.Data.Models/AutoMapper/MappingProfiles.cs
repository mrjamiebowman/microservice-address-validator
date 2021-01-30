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
            CreateMap<Address, ValidatedAddress>();

            CreateMap<AddressValidatorRequest, AddressValidatorResult>()
                .ForMember(m => m.AddressValidatorService,
                    opt => 
                        opt.MapFrom((src, dst, _, context) => 
                            context.Options.Items["AddressValidatorService"]
                        )
                );
        }
    }
}
