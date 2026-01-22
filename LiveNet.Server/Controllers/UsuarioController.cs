using LiveNet.Services.Dtos;
using LiveNet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace LiveNet.Api.Controllers;

[ApiController]
[Route( "Api/[Controller]" )]

public class UsuarioController( IUsuarioService service ) : ControllerBase
{

    private readonly IUsuarioService _service = service;

    [Authorize( Roles = "Admin" )]
    [HttpGet( "Buscar" )]
    public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetAsync()
    {
        var usuarios = await _service.BuscarUsuariosAsync();
        if ( !usuarios.IsNullOrEmpty() )
            return usuarios;
        else
            return NotFound( "Nenhum usuário" );
    }

    [Authorize]
    [HttpPost( "Criar" )]
    public async Task<ActionResult> PostAsync( UsuarioDto usuario )
    {
        if ( ModelState.IsValid )
        {
            await _service.CriarUsuarioAsync( usuario );
            return Created();
        }
        else
            return BadRequest();
    }

    [Authorize]
    [HttpPatch( "Editar" )]
    public async Task<ActionResult> PatchAsync( UsuarioDto usuario, Guid id )
    {
        if ( ModelState.IsValid )
        {
            var retorno = await _service.EditarUsuarioAsync( usuario, id );
            if ( retorno )
                return Ok();
            else
                return BadRequest();
        }
        else
            return BadRequest();
    }

    [Authorize( Roles = "Admin" )]
    [HttpDelete( "Deletar" )]
    public async Task<ActionResult> DeleteAsync( Guid id )
    {
        var retorno = await _service.DeletarUsuariosAsync( id );
        if ( retorno )
            return Ok();
        else
            return BadRequest();
    }
}