using LiveNet.Database.Context;
using LiveNet.Domain.Models;
using LiveNet.Services.Dtos;
using LiveNet.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LiveNet.Services.Services;

public class InteresseService(ApplicationDbContext context, IUsuarioAtualService usuarioAtualService) : IInteresseService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IUsuarioAtualService _usuarioAtualService = usuarioAtualService;

    public async Task<List<InteresseDto>> BuscarServicosAsync()
    {
        return await _context.Interesses.AsNoTracking()
            .Select(i => new InteresseDto
            {
                Id = i.Id,
                Interesse = i.Interesse
            }).ToListAsync();
    }

    public async Task CriarServicosAsync(InteresseModel interesse)
    {
        _context.Interesses.Add(interesse);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> AtualizarInteresseAsync(InteresseModel interesse, Guid id)
    {
        var filtro = await _context.Interesses.FirstOrDefaultAsync(x => x.Id == id);
        if (filtro == null)
            return false;

        filtro.Interesse = interesse.Interesse;
        filtro.UpdatedAt = DateTime.Now;
        filtro.UpdatedBy = _usuarioAtualService.UsuarioId;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExcluirInteresseAsync(Guid id)
    {
        var filtro = await _context.Interesses.FirstOrDefaultAsync(x => x.Id == id);
        if (filtro == null)
            return false;

        filtro.IsDeleted = true;
        filtro.DeletedBy = _usuarioAtualService.UsuarioId;
        filtro.DeletedAt = DateTimeOffset.Now;
        return true;
    }
}