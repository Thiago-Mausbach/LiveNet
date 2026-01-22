using LiveNet.Services.Dtos;

namespace LiveNet.Services.Interfaces;

public interface IServicoService
{
    Task<List<ServicoDto>> BuscarServicosAsync();
    Task CriarServicoAsync( ServicoDto servico );
    Task<bool> AtualizarServicoAsync( ServicoDto servico, Guid id );
    Task<bool> DeletarServicoAsync( Guid id );
}
