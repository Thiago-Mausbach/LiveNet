using LiveNet.Api.Mapping;
using LiveNet.Api.ViewModels;
using LiveNet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace LiveNet.Api.Controllers;

[Route("Api/[controller]")]
[ApiController]
public class ContatoController(IContatoService contato) : ControllerBase
{
    private readonly IContatoService _service = contato;

    [HttpGet(Name = "BuscarContatos")]
    public async Task<ActionResult<IEnumerable<ContatoViewModel>>> GetAsync()
    {
        var contatos = await _service.BuscarContatosAsync();
        if (!contatos.IsNullOrEmpty())
            return contatos.Select(c => c.ToContatoDto()).ToList();
        else
            return BadRequest();
    }

    [HttpPost("Importar", Name = "ImportarContatosCsv")]
    public async Task<IActionResult> PostImportAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Arquivo inválido");


        var ret = await _service.UploadListaAsync(file);

        return Ok(new
        {
            totalImportados = ret.TotalImportados,
            emailsDuplicados = ret.EmailsDuplicados
        });

    }

    [HttpPost("Adicionar", Name = "AdicionarContatosManual")]
    public async Task<IActionResult> PostAsync(ContatoViewModel contato, Guid empresaId)
    {
        if (ModelState.IsValid)
            return BadRequest();

        var model = ContatoMapper.ToContatoModel(contato, empresaId);
        await _service.CriarContatoManualAsync(model);
        return Ok();
    }

    [HttpPatch("{Id}", Name = "AtualizarContato")]
    public async Task<ActionResult> PatchAsync(Guid id, ContatoViewModel contato, Guid empresaId)
    {
        if (ModelState.IsValid)
            return BadRequest();

        var model = ContatoMapper.ToContatoModel(contato, empresaId);
        var retorno = await _service.AtualizarContatoAsync(id, model);
        if (retorno)
            return Ok();
        else
            return BadRequest();
    }

    [HttpDelete("{Id}", Name = "DeletarContato")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var retorno = await _service.ExcluirContatoAsync(id);
        if (retorno)
            return Ok();
        else
            return BadRequest();
    }
}