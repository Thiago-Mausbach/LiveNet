using LiveNet.Api.Mapping;
using LiveNet.Api.ViewModels;
using LiveNet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace LiveNet.Api.Controllers;

[Route("Api/[controller]")]
[ApiController]
public class EmpresaController(IEmpresaService service) : ControllerBase
{
    private readonly IEmpresaService _serivce = service;

    [Authorize]
    [HttpGet("Buscar")]
    public async Task<ActionResult<IEnumerable<EmpresaViewModel>>> GetAsync()
    {
        var ret = await _serivce.ListarEmpresasAsync();
        if (!ret.IsNullOrEmpty())
        {
            return ret.Select(r => r.ToEmpresaVm()).ToList();
        }
        else
            return BadRequest();
    }

    [Authorize]
    [HttpPost("Criar")]
    public async Task<ActionResult> PostAsync(EmpresaViewModel empresa)
    {
        var model = EmpresaMapper.ToEmpresaModel(empresa);
        var ret = await _serivce.CriarEmpresaAsync(model);
        if (ret)
            return Ok();
        else
            return BadRequest();
    }

    [Authorize]
    [HttpPatch("Editar")]
    public async Task<ActionResult> PatchAsync(Guid id, EmpresaViewModel empresa)
    {
        var model = EmpresaMapper.ToEmpresaModel(empresa);
        var ret = await _serivce.AtualizarEmpresaAsync(id, model);
        if (ret)
            return Ok();
        else
            return BadRequest();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("Deletar")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        var ret = await _serivce.RemoverEmpresaAsync(id);
        if (ret)
            return Ok();
        else
            return BadRequest();
    }
}

