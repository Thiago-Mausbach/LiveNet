using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveNet.Domain.Models;

public class EmpresaModel
{
    public Guid Id { get; set; }
    public string Cnpj { get; set; } = null!;
    public string Nome { get; set; } = null!;
    public List<ContatoModel> Contatos { get; set; } = new();
}

