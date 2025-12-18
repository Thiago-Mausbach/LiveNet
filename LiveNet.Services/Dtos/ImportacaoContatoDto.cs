namespace LiveNet.Services.Dtos;

public class ImportacaoContatoDto
{
    public int TotalImportados { get; set; }
    public List<string>? EmailsDuplicados { get; set; }
}
