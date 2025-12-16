using LiveNet.Domain;
using LiveNet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiveNet.Api.Controllers;

[Route("Api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.LoginAsync(model);
        if (!result.Sucesso)
            return Unauthorized(result.Mensagem);

        return Ok(new { token = result.Token });
    }
}

