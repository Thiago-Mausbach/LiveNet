using LiveNet.Services.Dtos;

namespace LiveNet.Services.Interfaces;

public interface IInteresseService
{
    Task<List<InteresseDto>> BuscarServicosAsync();
    Task CriarInteresseAsync( InteresseDto interesse );
    Task<bool> AtualizarInteresseAsync( InteresseDto interesse, Guid id );
    Task<bool> ExcluirInteresseAsync( Guid id );
}