using AutoMapper;
using LiveNet.Domain.Models;
using LiveNet.Services.Dtos;

namespace LiveNet.Services.Mapping;
public class EmpresaMapper : Profile
{
    public EmpresaMapper()
    {
        CreateMap<EmpresaModel, EmpresaDto>();
        CreateMap<EmpresaDto, EmpresaModel>();
    }

}
