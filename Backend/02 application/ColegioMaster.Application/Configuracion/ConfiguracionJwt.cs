namespace ColegioMaster.Application.Configuracion;

public class ConfiguracionJwt
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string ClaveSecreta { get; set; } = string.Empty;
    public int MinutosExpiracion { get; set; } = 120;
}
