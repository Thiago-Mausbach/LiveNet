using LiveNet.Domain.Models;
using LiveNet.Services.Dtos;

namespace LiveNet.Services.Interfaces;

public interface IInteresseService
{
    Task<List<InteresseDto>> BuscarServicosAsync();
    Task CriarServicosAsync(InteresseModel interesse);
    Task<bool> AtualizarInteresseAsync(InteresseModel interesse, Guid id);
    Task<bool> ExcluirInteresseAsync(Guid id);
}
