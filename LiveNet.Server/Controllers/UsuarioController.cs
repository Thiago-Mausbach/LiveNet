using LiveNet.Api.Mapping;
using LiveNet.Api.ViewModels;
using LiveNet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace LiveNet.Api.Controllers;

[ApiController]
[Route("Api/[Controller]")]

public class UsuarioController(IUsuarioService service) : ControllerBase
{

    private readonly IUsuarioService _service = service;

    [HttpGet("Buscar")]
    public async Task<ActionResult<IEnumerable<UsuarioViewModel>>> GetAsync()
    {
        var usuarios = await _service.BuscarUsuariosAsync();
        if (!usuarios.IsNullOrEmpty())
            return usuarios.Select(u => u.ToUsuarioVm()).ToList();
        else
            return NotFound("Nenhum usuário");
    }

    [HttpPost("Criar")]
    public async Task<ActionResult> PostAsync(UsuarioViewModel usuario)
    {
        if (ModelState.IsValid)
        {
            var model = UsuarioMapper.ToUsuarioModel(usuario);
            await _service.CriarUsuarioAsync(model);
            return Created();
        }
        else
            return BadRequest();
    }
    [HttpPatch("Editar")]
    public async Task<ActionResult> PatchAsync(UsuarioViewModel usuario, Guid id)
    {
        if (ModelState.IsValid)
        {
            var model = UsuarioMapper.ToUsuarioModel(usuario);
            var retorno = await _service.EditarUsuarioAsync(model, id);
            if (retorno)
                return Ok();
            else
                return BadRequest();
        }
        else
            return BadRequest();
    }

    [HttpDelete("Deletar")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        var retorno = await _service.DeletarUsuariosAsync(id);
        if (retorno)
            return Ok();
        else
            return BadRequest();
    }
}