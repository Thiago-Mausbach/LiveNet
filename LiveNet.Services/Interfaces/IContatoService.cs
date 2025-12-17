using LiveNet.Domain.Models;
using LiveNet.Services.Dtos;
using Microsoft.AspNetCore.Http;

namespace LiveNet.Services.Interfaces;

public interface IContatoService
{
    Task<List<ContatoDto>> BuscarContatosAsync();
    Task<bool> UploadListaAsync(IFormFile file, string nome);
    Task<bool> CriarContatoManualAsync(ContatoModel contato);
    Task<bool> AtualizarContatoAsync(Guid id, ContatoModel contato);
    Task<bool> ExcluirContatoAsync(int id);
}
