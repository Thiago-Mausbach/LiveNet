using LiveNet.Api.Mapping;
using LiveNet.Api.ViewModels;
using LiveNet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiveNet.Api.Controllers;

[Route("Api/[controller]")]
[ApiController]
public class InteresseController(IInteresseService service) : ControllerBase
{
    private readonly IInteresseService _service = service;

    [Authorize]
    [HttpGet("Buscar")]
    public async Task<IEnumerable<InteresseViewModel>> GetAsync()
    {
        var interesses = await _service.BuscarServicosAsync();
        return interesses.Select(i => i.ToInteresseVm()).ToList();
    }

    [Authorize]
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

    [Authorize]
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

    [Authorize(Roles = "Admin")]
    [HttpDelete("Deletar")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await _service.ExcluirInteresseAsync(id);
        return Ok();
    }
}