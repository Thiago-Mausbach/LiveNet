using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveNet.Domain.Models;

public class ContatoInteresseModel
{
    public Guid Id { get; set; }
    public Guid ContatoId { get; set; }
    public Guid InteresseId { get; set; }
    public ContatoModel Contato { get; set; } = null!;
    public InteresseModel Interesse { get; set; } = null!;
}
