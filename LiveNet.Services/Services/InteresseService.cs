using AutoMapper;
using LiveNet.Database.Context;
using LiveNet.Domain.Models;
using LiveNet.Services.Dtos;
using LiveNet.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LiveNet.Services.Services;

public class InteresseService( ApplicationDbContext context, IUsuarioAtualService usuarioAtualService, IMapper mapper ) : IInteresseService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IUsuarioAtualService _usuarioAtualService = usuarioAtualService;
    private readonly IMapper _mapper = mapper;

    public async Task<List<InteresseDto>> BuscarServicosAsync()
    {
        return await _context.Interesses.AsNoTracking()
            .Select( i => new InteresseDto
            {
                Id = i.Id,
                Interesse = i.Interesse
            } ).ToListAsync();
    }

    public async Task CriarInteresseAsync( InteresseDto interesse )
    {
        var interesseM = _mapper.Map<InteresseModel>( interesse );

        _context.Interesses.Add( interesseM );
        await _context.SaveChangesAsync();
    }

    public async Task<bool> AtualizarInteresseAsync( InteresseDto interesse, Guid id )
    {
        var filtro = await _context.Interesses.FindAsync( id )
            ?? throw new KeyNotFoundException( "Interesse não encontrado" );

        var usuarioId = _usuarioAtualService.UsuarioId
          ?? throw new UnauthorizedAccessException();

        filtro.Interesse = interesse.Interesse;
        filtro.UpdatedAt = DateTime.Now;
        filtro.UpdatedBy = usuarioId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExcluirInteresseAsync( Guid id )
    {
        var filtro = await _context.Interesses.FindAsync( id )
        ?? throw new KeyNotFoundException( "Interesse não encontrado" );

        var usuarioId = _usuarioAtualService.UsuarioId
            ?? throw new UnauthorizedAccessException();


        filtro.IsDeleted = true;
        filtro.DeletedAt = DateTimeOffset.Now;
        filtro.DeletedBy = usuarioId;

        await _context.SaveChangesAsync();
        return true;
    }
}