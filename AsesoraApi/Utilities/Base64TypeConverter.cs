using AsesoraApi.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AsesoraApi.Utilities
{
    public class Base64TypeConverter : ITypeConverter<byte[], String>
    {
        public string Convert(byte[] source, String destination, ResolutionContext context)
        {
            using (MemoryStream m = new MemoryStream())
            {
                String base64String = System.Convert.ToBase64String(source);
                return base64String;
            }
        }
    }
    public class ByteArrayTypeConverter : ITypeConverter<string, byte[]>
    {
        public byte[] Convert(string source, byte[] destination, ResolutionContext context)
        {
            byte[] imageBytes = System.Convert.FromBase64String(source);
            return imageBytes;
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<string, byte[]>().ConvertUsing(new ByteArrayTypeConverter());
            CreateMap<byte[], string>().ConvertUsing(new Base64TypeConverter());

            CreateMap<UserxImageDto, UserxImage>();
            CreateMap<UserxImage, Userx>();
        }
    }
}
