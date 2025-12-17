using LiveNet.Api.Mapping;
using LiveNet.Api.ViewModels;
using LiveNet.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace LiveNet.Api.Controllers;

[Route("Api/[controller]")]
[ApiController]
public class InteresseController(InteresseService service) : ControllerBase
{
    private readonly InteresseService _service = service;

    [HttpGet]
    public async Task<IEnumerable<InteresseViewModel>> GetAsync()
    {
        var interesses = await _service.BuscarServicosAsync();
        return interesses.Select(i => i.ToInteresseDto()).ToList();
    }

    [HttpPost]
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

    [HttpPatch]
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

    [HttpDelete]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await _service.ExcluirInteresseAsync(id);
        return Ok();
    }
}