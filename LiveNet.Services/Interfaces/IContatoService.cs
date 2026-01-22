using LiveNet.Services.Dtos;
using Microsoft.AspNetCore.Http;

namespace LiveNet.Services.Interfaces;

public interface IContatoService
{
    Task<List<ContatoDto>> BuscarContatosAsync( Guid? usuarioId );
    Task<ImportacaoContatoDto> UploadListaAsync( IFormFile file );
    Task<bool> CriarContatoManualAsync( ContatoDto contato );
    Task<bool> AtualizarContatoAsync( Guid id, ContatoDto contato );
    Task<bool> ExcluirContatoAsync( int id );
}