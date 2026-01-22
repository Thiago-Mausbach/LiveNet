using AutoMapper;
using LiveNet.Domain.Models;
using LiveNet.Services.Dtos;

namespace LiveNet.Services.Mapping;
public class ContatoMapper : Profile
{
    public ContatoMapper()
    {
        CreateMap<ContatoDto, ContatoModel>();
        CreateMap<ContatoModel, ContatoDto>();
    }
}
