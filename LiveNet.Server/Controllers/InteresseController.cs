using LiveNet.Api.Mapping;
using LiveNet.Api.ViewModels;
using LiveNet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LiveNet.Api.Controllers;

[Route("Api/[controller]")]
[ApiController]
public class InteresseController(IInteresseService service) : ControllerBase
{
    private readonly IInteresseService _service = service;

    [HttpGet("Buscar")]
    public async Task<IEnumerable<InteresseViewModel>> GetAsync()
    {
        var interesses = await _service.BuscarServicosAsync();
        return interesses.Select(i => i.ToInteresseVm()).ToList();
    }

    [HttpPost("Criar")]
    public async Task<ActionResult> PostAsync(InteresseViewModel interesseViewModel)
    {
        if (ModelState.IsValid)
        {
            var model = InteresseMapper.ToInteresseModel(interesseViewModel);
            await _service.CriarServicosAsync(model);
            return Ok();
        }
        else
            return BadRequest();
    }

    [HttpPatch("Editar")]
    public async Task<ActionResult> PatchAsync(InteresseViewModel interesseViewModel, Guid id)
    {
        if (ModelState.IsValid)
        {
            var model = InteresseMapper.ToInteresseModel(interesseViewModel);
            await _service.AtualizarInteresseAsync(model, id);
            return Ok();
        }
        return BadRequest();
    }

    [HttpDelete("Deletar")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await _service.ExcluirInteresseAsync(id);
        return Ok();
    }
}