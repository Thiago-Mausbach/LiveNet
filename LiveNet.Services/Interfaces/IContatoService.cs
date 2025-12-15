using LiveNet.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace LiveNet.Services.Interfaces;

public interface IContatoService
{
    Task<List<ContatoModel>> BuscarContatosAsync();
    Task UploadListaAsync(IFormFile file, string nome);
    Task CriarContatoManualAsync(ContatoModel contato);
    Task<ContatoModel> AtualizarContatoAsync(int id, ContatoModel contato);
    Task<int> ExcluirContatoAsync(int id);
}
