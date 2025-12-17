namespace LiveNet.Domain.Models;

public class ContatoServicoModel
{
    public Guid ServicoId { get; set; }
    public Guid ContatoId { get; set; }
    public ServicoModel Servico { get; set; }
    public ContatoModel Contato { get; set; }

}
