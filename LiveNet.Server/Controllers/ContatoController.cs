using LiveNet.Domain.Models;
using LiveNet.Domain.ViewModels;
using LiveNet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LiveNet.Api.Controllers;

[Route("Api/[controller]")]
[ApiController]
public class ContatoController : ControllerBase
{
    private readonly IContatoService _service;

    public ContatoController(IContatoService contato)
    {
        _service = contato;
    }

    [HttpGet(Name = "BuscarContatos")]
    public async Task<ActionResult<IEnumerable<ContatoViewModel>>> GetAsync()
    {
        var contatos = await _service.BuscarContatosAsync();
        if (contatos == null || contatos.Count == 0)
            return Ok();
        else
            return BadRequest();
    }

    [HttpPost("Importar", Name = "ImportarContatosCsv")]
    public async Task<IActionResult> PostImportAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Arquivo inválido");

        using var stream = file.OpenReadStream();
        await _service.CriarContatosListaAsync(stream, file.FileName);

        return Ok("Importação concluída");

    }

    [HttpPost("Adicionar", Name = "AdicionarContatosManual")]
    public async Task PostAsync(ContatoModel contato)
    {
        await _service.CriarContatoManualAsync(contato);
    }

    [HttpPatch("{Id}", Name = "AtualizarContato")]
    public async Task<ActionResult> PatchAsync(int id, ContatoModel contato)
    {
        var retorno = await _service.AtualizarContatoAsync(id, contato);
        if (retorno != null)
            return Ok();
        else
            return BadRequest();
    }

    [HttpDelete("{Id}", Name = "DeletarContato")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var retorno = await _service.ExcluirContatoAsync(id);
        if (retorno == 1)
            return Ok();
        else
            return BadRequest();
    }
}