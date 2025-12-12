using LiveNet.Domain.Models;
using LiveNet.Domain.ViewModels;

namespace LiveNet.Services.Interfaces;

public interface IContatoService
{
    Task<List<ContatoViewModel>> BuscarContatosAsync();
    Task CriarContatosListaAsync(Stream stream, string nome);
    Task CriarContatoManualAsync(ContatoModel contato);
    Task<ContatoModel> AtualizarContatoAsync(int id, ContatoModel contato);
    Task<int> ExcluirContatoAsync(int id);
}
