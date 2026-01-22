using LiveNet.Services.Dtos;
using LiveNet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiveNet.Api.Controllers;


[Route( "Api/[controller]" )]
[ApiController]
public class FavoritoController( IFavoritoService favoritoService ) : ControllerBase
{
    private readonly IFavoritoService _favoritoService = favoritoService;

    [Authorize]
    [HttpGet( "Buscar" )]
    public async Task<IEnumerable<ContatoDto>> GetAsync()
    {
        var retorno = await _favoritoService.ListarFavoritosAsync();
        return retorno;
    }

    [Authorize]
    [HttpPost( "AdicionarRemover" )]
    public async Task<ActionResult> FavoritarAsync( Guid contatoId, Guid usuarioId )
    {
        var retorno = await _favoritoService.ToggleAsync( contatoId, usuarioId );
        if ( retorno )
            return Ok( "Contato adicionado aos favoritos" );
        else
            return Ok( "Contato removido dos favoritos" );
    }
}
