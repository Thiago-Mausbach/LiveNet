using LiveNet.Domain.Models;

namespace LiveNet.Services.Interfaces;

public interface IContatoService
{
    Task<List<ContatoModel>> BuscarContatosAsync();
    Task CriarContatosListaAsync(Stream stream, string nome);
    Task CriarContatoManualAsync(ContatoModel contato);
    Task<ContatoModel> AtualizarContatoAsync(int id, ContatoModel contato);
    Task<int> ExcluirContatoAsync(int id);
}
