using ColegioMaster.Infrastructure.Repositorios.Modelos;

namespace ColegioMaster.Application.Dtos.Comun;

public class ConsultaPaginadaDto
{
    public int NumeroPagina { get; set; } = 1;
    public int TamanioPagina { get; set; } = 20;
    public List<ItemFilterRequest> Filters { get; set; } = new();
}

public class ResultadoPaginadoDto<T>
{
    public int NumeroPagina { get; set; }
    public int TamanioPagina { get; set; }
    public int TotalRegistros { get; set; }
    public int TotalPaginas { get; set; }
    public List<T> Registros { get; set; } = new();
}
