namespace ColegioMaster.Infrastructure.Repositorios.Modelos;

public class ConsultaRepositorioRequest
{
    public int NumeroPagina { get; set; } = 1;
    public int TamanioPagina { get; set; } = 20;
    public List<ItemFilterRequest> Filters { get; set; } = new();
}

public class ResultadoPaginadoRepositorio<T>
{
    public int TotalRegistros { get; set; }
    public List<T> Registros { get; set; } = new();
}
