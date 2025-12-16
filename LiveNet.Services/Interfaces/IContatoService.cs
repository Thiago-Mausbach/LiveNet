using LiveNet.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace LiveNet.Services.Interfaces;

public interface IContatoService
{
    Task<List<ContatoModel>> BuscarContatosAsync();
    Task<bool> UploadListaAsync(IFormFile file, string nome);
    Task<bool> CriarContatoManualAsync(ContatoModel contato);
    Task<bool> AtualizarContatoAsync(Guid id, ContatoModel contato);
    Task<bool> ExcluirContatoAsync(int id);
}
