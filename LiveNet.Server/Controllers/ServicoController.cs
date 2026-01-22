using LiveNet.Services.Dtos;
using LiveNet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace LiveNet.Api.Controllers;


[ApiController]
[Route( "Api/[Controller]" )]

public class ServicoController( IServicoService service ) : ControllerBase
{

    private readonly IServicoService _service = service;

    [Authorize]
    [HttpGet( "Buscar" )]
    public async Task<ActionResult<IEnumerable<ServicoDto>>> GetAsync()
    {
        var servicos = await _service.BuscarServicosAsync();
        if ( !servicos.IsNullOrEmpty() )
            return servicos;
        else
            return NotFound();
    }

    [Authorize]
    [HttpPost( "Criar" )]
    public async Task<ActionResult> PostAsync( ServicoDto servico )
    {
        if ( servico == null )
            return BadRequest();

        await _service.CriarServicoAsync( servico );
        return Created();
    }

    [Authorize]
    [HttpPatch( "Editar" )]
    public async Task<ActionResult> PatchAsync( ServicoDto servico, Guid id )
    {
        var retorno = await _service.AtualizarServicoAsync( servico, id );
        if ( retorno )
            return Ok();
        else
            return BadRequest();
    }

    [Authorize( Roles = "Admin" )]
    [HttpDelete( "Deletar" )]
    public async Task<ActionResult> DeleteAsync( Guid id )
    {
        var retorno = await _service.DeletarServicoAsync( id );
        if ( retorno )
            return Ok();

        return BadRequest();
    }
}