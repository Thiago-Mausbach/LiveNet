using AutoMapper;
using LiveNet.Domain.Models;
using LiveNet.Services.Dtos;

namespace LiveNet.Services.Mapping;
public class InteresseMapper : Profile
{
    public InteresseMapper()
    {
        CreateMap<InteresseModel, InteresseDto>();
        CreateMap<InteresseDto, InteresseModel>();
    }

}
