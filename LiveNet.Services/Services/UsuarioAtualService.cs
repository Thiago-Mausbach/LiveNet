using LiveNet.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace LiveNet.Services.Services;

public class UsuarioAtualService(IHttpContextAccessor httpContextAccessor) : IUsuarioAtualService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public Guid? UsuarioId => Guid.TryParse(
               _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
               out var guid)
           ? guid
           : null;
}