using LiveNet.Api.Mapping;
using LiveNet.Api.ViewModels;
using LiveNet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiveNet.Api.Controllers;


[Route("Api/[controller]")]
[ApiController]
public class FavoritoController(IFavoritoService favoritoService) : ControllerBase
{
    private readonly IFavoritoService _favoritoService = favoritoService;

    [Authorize]
    [HttpGet("Buscar")]
    public async Task<IEnumerable<ContatoViewModel>> GetAsync()
    {
        var retorno = await _favoritoService.ListarFavoritosAsync();
        return retorno.Select(r => r.ContatoDtoToVm()).ToList();
    }

    [Authorize]
    [HttpPost("AdicionarRemover")]
    public async Task<ActionResult> FavoritarAsync(Guid contatoId, Guid usuarioId)
    {
        var retorno = await _favoritoService.ToggleAsync(contatoId, usuarioId);
        if (retorno)
            return Ok("Contato adicionado aos favoritos");
        else
            return Ok("Contato removido dos favoritos");
    }
}
