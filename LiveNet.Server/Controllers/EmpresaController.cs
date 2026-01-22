using LiveNet.Services.Dtos;
using LiveNet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace LiveNet.Api.Controllers;

[Route( "Api/[controller]" )]
[ApiController]
public class EmpresaController( IEmpresaService service ) : ControllerBase
{
    private readonly IEmpresaService _serivce = service;

    [Authorize]
    [HttpGet( "Buscar" )]
    public async Task<ActionResult<IEnumerable<EmpresaDto>>> GetAsync()
    {
        var ret = await _serivce.ListarEmpresasAsync();
        if ( ret.IsNullOrEmpty() )
        {
            return BadRequest();
        }
        else
            return ret;
    }

    [Authorize]
    [HttpPost( "Criar" )]
    public async Task<ActionResult> PostAsync( EmpresaDto empresa )
    {
        var ret = await _serivce.CriarEmpresaAsync( empresa );
        if ( ret )
            return Ok();
        else
            return BadRequest();
    }

    [Authorize]
    [HttpPatch( "Editar" )]
    public async Task<ActionResult> PatchAsync( Guid id, EmpresaDto empresa )
    {
        var ret = await _serivce.AtualizarEmpresaAsync( id, empresa );
        if ( ret )
            return Ok();
        else
            return BadRequest();
    }

    [Authorize( Roles = "Admin" )]
    [HttpDelete( "Deletar" )]
    public async Task<ActionResult> DeleteAsync( Guid id )
    {
        var ret = await _serivce.RemoverEmpresaAsync( id );
        if ( ret )
            return Ok();
        else
            return BadRequest();
    }
}

