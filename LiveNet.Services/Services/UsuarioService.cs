using LiveNet.Database.Context;
using LiveNet.Domain.Models;
using LiveNet.Infrastructure;
using LiveNet.Services.Dtos;
using LiveNet.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LiveNet.Services.Services;

public class UsuarioService(ApplicationDbContext context, IUsuarioAtualService usuarioAtualService, IPasswordHasher<UsuarioModel> hasher) : IUsuarioService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IUsuarioAtualService _usuarioAtualService = usuarioAtualService;
    private readonly IPasswordHasher<UsuarioModel> _hasher = hasher;

    public async Task<List<UsuarioDto>> BuscarUsuariosAsync()
    {
        return await _context.Usuarios
            .Select(u => new UsuarioDto
            {
                Nome = u.Nome,
                Email = u.Email,
                Id = u.Id,
                Senha = u.Senha,
            }).ToListAsync();
    }

    public async Task CriarUsuarioAsync(UsuarioModel usuario)
    {
        usuario.Senha = _hasher.HashPassword(usuario, usuario.Senha);
        _context.Add(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> EditarUsuarioAsync(UsuarioModel usuario, Guid id)
    {
        var original = await _context.Usuarios.FindAsync(id)
        ?? throw new KeyNotFoundException("Usuario não encontrado");

        var usuarioId = _usuarioAtualService.UsuarioId
            ?? throw new UnauthorizedAccessException();

        EntityDiffValidate.ValidarDif(original, usuario);

        original.UpdatedAt = DateTimeOffset.Now;
        original.UpdatedBy = usuarioId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeletarUsuariosAsync(Guid id)
    {
        var servico = await _context.Usuarios.FindAsync(id)
        ?? throw new KeyNotFoundException("Uusario não encontrado");

        var usuarioId = _usuarioAtualService.UsuarioId
            ?? throw new UnauthorizedAccessException();

        servico.IsDeleted = true;
        servico.DeletedAt = DateTime.Now;
        servico.DeletedBy = usuarioId;
        await _context.SaveChangesAsync();
        return true;
    }
}