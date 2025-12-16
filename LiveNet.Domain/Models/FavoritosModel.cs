using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveNet.Domain.Models;

public class FavoritosModel
{
    public required Guid UsuarioId { get; set; }
    public Guid ContatoId { get; set; }
    public UsuarioModel Usuario { get; set; }
    public ContatoModel Contato { get; set; }
}