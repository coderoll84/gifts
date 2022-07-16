using AutoMapper;
using Mvc.Data.Models;
using Mvc.Models;

namespace Mvc.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<OcasionModel, Ocasione>().ReverseMap();
        }
    }
}
