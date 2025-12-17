using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveNet.Domain.Models;

public class ContatoServicoModel
{
    public int ServicoId { get; set; }
    public Guid ContatoId { get; set; }
    public ServicoModel Servico { get; set; }
    public ContatoModel Contato { get; set; }

}
