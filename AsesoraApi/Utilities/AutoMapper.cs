using AsesoraApi.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsesoraApi.Utilities
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<UserxImage, Userx>()
                .ReverseMap();
        }
    }
}
