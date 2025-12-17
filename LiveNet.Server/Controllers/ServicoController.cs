using LiveNet.Api.Mapping;
using LiveNet.Api.ViewModels;
using LiveNet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace LiveNet.Api.Controllers;


[ApiController]
[Route("Api/[Controller]")]

public class ServicoController(IServicoService service) : ControllerBase
{

    private readonly IServicoService _service = service;

    [HttpGet(Name = "BuscarServico")]
    public async Task<ActionResult<IEnumerable<ServicoViewModel>>> GetAsync()
    {
        var servicos = await _service.BuscarServicosAsync();
        if (!servicos.IsNullOrEmpty())
            return servicos.Select(s => s.ToServicoDto()).ToList();
        else
            return NotFound();
    }

    [HttpPost(Name = "CriarServico")]
    public async Task<ActionResult> PostAsync(ServicoViewModel servico)
    {
        if (servico == null)
            return BadRequest();

        var model = ServicoMapper.ToServicoModel(servico);
        await _service.CriarServicoAsync(model);
        return Created();
    }

    [HttpPatch("{id}", Name = "AtualizarServico")]
    public async Task<ActionResult> PatchAsync(ServicoViewModel servico, Guid id)
    {
        var model = ServicoMapper.ToServicoModel(servico);
        var retorno = await _service.AtualizarServicoAsync(model, id);
        if (retorno)
            return Ok();
        else
            return BadRequest();
    }

    [HttpDelete("{id}", Name = "DeletarServico")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        var retorno = await _service.DeletarServicoAsync(id);
        if (retorno)
            return Ok();

        return BadRequest();
    }
}