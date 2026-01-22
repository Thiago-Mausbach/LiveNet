using LiveNet.Services.Dtos;
using LiveNet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiveNet.Api.Controllers;

[Route( "Api/[controller]" )]
[ApiController]
public class InteresseController( IInteresseService service ) : ControllerBase
{
    private readonly IInteresseService _service = service;

    [Authorize]
    [HttpGet( "Buscar" )]
    public async Task<IEnumerable<InteresseDto>> GetAsync()
    {
        var interesses = await _service.BuscarServicosAsync();
        return interesses;
    }

    [Authorize]
    [HttpPost( "Criar" )]
    public async Task<ActionResult> PostAsync( InteresseDto interesse )
    {
        if ( ModelState.IsValid )
        {
            await _service.CriarInteresseAsync( interesse );
            return Ok();
        }
        else
            return BadRequest();
    }

    [Authorize]
    [HttpPatch( "Editar" )]
    public async Task<ActionResult> PatchAsync( InteresseDto interesse, Guid id )
    {
        if ( ModelState.IsValid )
        {
            await _service.AtualizarInteresseAsync( interesse, id );
            return Ok();
        }
        return BadRequest();
    }

    [Authorize( Roles = "Admin" )]
    [HttpDelete( "Deletar" )]
    public async Task<ActionResult> DeleteAsync( Guid id )
    {
        await _service.ExcluirInteresseAsync( id );
        return Ok();
    }
}