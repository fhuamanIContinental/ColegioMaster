namespace ColegioMaster.Infrastructure.Repositorios.Modelos;

public class ItemFilterRequest
{
    public string Campo { get; set; } = string.Empty;
    public string Operador { get; set; } = "eq";
    public string? Valor { get; set; }
}
