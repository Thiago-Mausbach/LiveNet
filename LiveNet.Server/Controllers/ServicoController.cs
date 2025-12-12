using LiveNet.Domain.Models;
using LiveNet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LiveNet.Api.Controllers;


[ApiController]
[Route("Api/[Controller]")]

public class ServicoController : ControllerBase
{

    private readonly IServicoService _service;

    public ServicoController(IServicoService service)
    {
        _service = service;
    }

    [HttpGet(Name = "BuscarServico")]
    public async Task<ActionResult<IEnumerable<ServicoModel>>> GetAsync()
    {
        var servicos = await _service.BuscarServicosAsync();
        if (servicos == null || servicos.Count == 0)
            return NotFound();
        else
            return Ok(servicos);
    }

    [HttpPost(Name = "CriarServico")]
    public async Task<ActionResult> PostAsync(ServicoModel servico)
    {
        if (servico == null)
            return BadRequest();

        await _service.CriarServicoAsync(servico);
        return Created();
    }

    [HttpPatch("{id}", Name = "AtualizarServico")]
    public async Task<ActionResult> PatchAsync(ServicoModel servico)
    {
        var retorno = await _service.AtualizarServicoAsync(servico);
        if (retorno != null)
            return Ok();
        else
            return BadRequest();
    }

    [HttpDelete("{id}", Name = "DeletarServico")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var retorno = await _service.DeletarServicoAsync(id);
        if (retorno == 1)
            return Ok();

        return BadRequest();
    }
}