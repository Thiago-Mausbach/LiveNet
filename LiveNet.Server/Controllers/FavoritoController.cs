using LiveNet.Api.Mapping;
using LiveNet.Api.ViewModels;
using LiveNet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LiveNet.Api.Controllers;


[Route("Api/[controller]")]
[ApiController]
public class FavoritoController(IFavoritoService favoritoService) : ControllerBase
{
    private readonly IFavoritoService _favoritoService = favoritoService;

    [HttpGet]
    public async Task<IEnumerable<ContatoViewModel>> GetAsync()
    {
        var retorno = await _favoritoService.ListarFavoritosAsync();
        return retorno.Select(r => r.ToContatoDto()).ToList();
    }

    [HttpPost]
    public async Task<ActionResult> FavoritarAsync(Guid contatoId)
    {
        var retorno = await _favoritoService.ToggleAsync(contatoId);
        if (retorno)
            return Ok("Contato adicionado aos favoritos");
        else
            return Ok("Contato removido dos favoritos");
    }
}
