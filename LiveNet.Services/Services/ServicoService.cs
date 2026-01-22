using AutoMapper;
using LiveNet.Database.Context;
using LiveNet.Domain.Models;
using LiveNet.Infrastructure;
using LiveNet.Services.Dtos;
using LiveNet.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LiveNet.Services.Services;

internal class ServicoService( ApplicationDbContext context, IUsuarioAtualService usuarioAtualService, IMapper mapper ) : IServicoService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IUsuarioAtualService _usuarioAtualService = usuarioAtualService;
    private readonly IMapper _mapper = mapper;


    public async Task<List<ServicoDto>> BuscarServicosAsync()
    {
        return await _context.Servicos
            .Select( s => new ServicoDto
            {
                Id = s.Id,
                Servico = s.Servico
            } ).ToListAsync();
    }

    public async Task CriarServicoAsync( ServicoDto servico )
    {
        var servicoM = _mapper.Map<ServicoModel>( servico );

        _context.Servicos.Add( servicoM );
        await _context.SaveChangesAsync();
    }

    public async Task<bool> AtualizarServicoAsync( ServicoDto servico, Guid id )
    {
        var original = await _context.Servicos.FindAsync( id )
         ?? throw new KeyNotFoundException( "Serviço não encontrado" );

        var usuarioId = _usuarioAtualService.UsuarioId
    ?? throw new UnauthorizedAccessException();

        var servicoM = _mapper.Map<ServicoModel>( servico );
        EntityDiffValidate.ValidarDif( original, servicoM );

        original.UpdatedAt = DateTimeOffset.Now;
        original.UpdatedBy = usuarioId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeletarServicoAsync( Guid id )
    {
        var servico = await _context.Servicos.FindAsync( id )
        ?? throw new KeyNotFoundException( "Serviço não encontrado" );

        var usuarioId = _usuarioAtualService.UsuarioId
            ?? throw new UnauthorizedAccessException();

        servico.IsDeleted = true;
        servico.DeletedAt = DateTime.Now;
        servico.DeletedBy = usuarioId;

        await _context.SaveChangesAsync();
        return true;
    }
}