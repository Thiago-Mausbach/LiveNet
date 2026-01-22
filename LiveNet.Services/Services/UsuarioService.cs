using AutoMapper;
using LiveNet.Database.Context;
using LiveNet.Domain.Models;
using LiveNet.Infrastructure;
using LiveNet.Services.Dtos;
using LiveNet.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LiveNet.Services.Services;

public class UsuarioService( ApplicationDbContext context, IUsuarioAtualService usuarioAtualService, IPasswordHasher<UsuarioModel> hasher, IMapper mapper ) : IUsuarioService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IUsuarioAtualService _usuarioAtualService = usuarioAtualService;
    private readonly IPasswordHasher<UsuarioModel> _hasher = hasher;
    private readonly IMapper _mapper = mapper;

    public async Task<List<UsuarioDto>> BuscarUsuariosAsync()
    {
        var ehAdmin = await _context.Usuarios.FirstAsync( x => x.Id == _usuarioAtualService.UsuarioId );
        if ( ehAdmin.IsAdmin )
        {
            return await _context.Usuarios
                .Select( u => new UsuarioDto
                {
                    Nome = u.Nome,
                    Email = u.Email,
                    Id = u.Id,
                    Senha = u.Senha,
                } ).ToListAsync();
        }
        else
            throw new UnauthorizedAccessException();
    }

    public async Task CriarUsuarioAsync( UsuarioDto usuario )
    {
        var usuarioM = _mapper.Map<UsuarioModel>( usuario );

        usuario.Senha = _hasher.HashPassword( usuarioM, usuarioM.Senha );
        _context.Add( usuarioM );
        await _context.SaveChangesAsync();
    }

    public async Task<bool> EditarUsuarioAsync( UsuarioDto usuario, Guid id )
    {
        var original = await _context.Usuarios.FindAsync( id )
        ?? throw new KeyNotFoundException( "Usuario não encontrado" );

        var usuarioId = _usuarioAtualService.UsuarioId
            ?? throw new UnauthorizedAccessException();

        var usuarioAtual = await _context.Usuarios.FirstAsync( x => x.Id == usuarioId );

        if ( original.Id == _usuarioAtualService.UsuarioId || usuarioAtual.IsAdmin )
        {
            var usuarioM = _mapper.Map<UsuarioModel>( usuario );
            EntityDiffValidate.ValidarDif( original, usuarioM );

            original.UpdatedAt = DateTimeOffset.Now;
            original.UpdatedBy = usuarioId;

            await _context.SaveChangesAsync();
            return true;
        }
        else
            throw new UnauthorizedAccessException();
    }

    public async Task<bool> DeletarUsuariosAsync( Guid id )
    {
        var servico = await _context.Usuarios.FindAsync( id )
        ?? throw new KeyNotFoundException( "Uusario não encontrado" );

        var usuarioId = _usuarioAtualService.UsuarioId
            ?? throw new UnauthorizedAccessException();

        servico.IsDeleted = true;
        servico.DeletedAt = DateTime.Now;
        servico.DeletedBy = usuarioId;
        await _context.SaveChangesAsync();
        return true;
    }
}