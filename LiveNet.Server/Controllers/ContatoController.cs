using LiveNet.Services.Dtos;
using LiveNet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace LiveNet.Api.Controllers;

[Route( "Api/[controller]" )]
[ApiController]
public class ContatoController( IContatoService contato, IUsuarioAtualService usuario ) : ControllerBase
{
    private readonly IContatoService _service = contato;
    private readonly IUsuarioAtualService _usuarioAtualService = usuario;

    [Authorize]
    [HttpGet( "Buscar" )]
    public async Task<ActionResult<IEnumerable<ContatoDto>>> GetAsync()
    {
        var usuarioId = _usuarioAtualService.UsuarioId;
        var contatos = await _service.BuscarContatosAsync( usuarioId );
        if ( !contatos.IsNullOrEmpty() )
            return contatos;
        else
            return BadRequest();
    }

    [Authorize]
    [HttpPost( "Importar" )]
    public async Task<IActionResult> PostImportAsync( IFormFile file )
    {
        if ( file == null || file.Length == 0 )
            return BadRequest( "Arquivo inválido" );


        var ret = await _service.UploadListaAsync( file );

        return Ok( new
        {
            totalImportados = ret.TotalImportados,
            emailsDuplicados = ret.EmailsDuplicados
        } );

    }

    [Authorize]
    [HttpPost( "Adicionar" )]
    public async Task<IActionResult> PostAsync( ContatoDto contato, Guid empresaId )
    {
        if ( !ModelState.IsValid )
            return BadRequest();

        await _service.CriarContatoManualAsync( contato );
        return Ok();
    }

    [Authorize]
    [HttpPatch( "Atualizar" )]
    public async Task<ActionResult> PatchAsync( Guid id, ContatoDto contato, Guid empresaId )
    {
        if ( ModelState.IsValid )
            return BadRequest();

        var retorno = await _service.AtualizarContatoAsync( id, contato );
        if ( retorno )
            return Ok();
        else
            return BadRequest();
    }

    [Authorize( Roles = "Admin" )]
    [HttpDelete( "Deletar" )]
    public async Task<ActionResult> DeleteAsync( int id )
    {
        var retorno = await _service.ExcluirContatoAsync( id );
        if ( retorno )
            return Ok();
        else
            return BadRequest();
    }
}