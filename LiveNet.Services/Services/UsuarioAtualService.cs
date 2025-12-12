using LiveNet.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LiveNet.Services.Services;

public class UsuarioAtualService : IUsuarioAtualService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UsuarioAtualService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UsuarioId => Guid.TryParse(
               _httpContextAccessor.HttpContext?.User.FindFirst("sub")?.Value,
               out var guid)
           ? guid
           : null;
}