using LiveNet.Domain.Models;
using LiveNet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LiveNet.Api.Controllers;

[ApiController]
[Route("Api/[Controller]")]

public class UsuarioController : ControllerBase
{

    private readonly IUsuarioService _service;

    public UsuarioController(IUsuarioService service)
    {
        _service = service;
    }

    [HttpGet(Name = "BuscarUsuario")]
    public async Task<ActionResult<IEnumerable<UsuarioModel>>> GetAsync()
    {
        var usuarios = await _service.BuscarUsuariosAsync();
        if (usuarios == null || usuarios.Count == 0)
            return NotFound("Nenhum usuário");

        return Ok(usuarios);
    }

    [HttpPost(Name = "CriarUsuario")]
    public async Task<ActionResult> PostAsync(UsuarioModel usuario)
    {
        if (usuario == null)
            return BadRequest();

        await _service.CriarUsuarioAsync(usuario);
        return Created();
    }

    [HttpPatch("{id}", Name = "AtualizarUsuario")]
    public async Task<ActionResult> PatchAsync(UsuarioModel usuario)
    {
        var retorno = await _service.EditarUsuarioAsync(usuario);
        if (retorno != null)
            return Ok();
        else
            return BadRequest();
    }

    [HttpDelete("{id}", Name = "DeletarUsuario")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        var retorno = await _service.DeletarUsuariosAsync(id);
        if (retorno == 1)
            return Ok();
        else
            return BadRequest();
    }
}

