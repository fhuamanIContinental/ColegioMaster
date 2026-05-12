using System.Security.Cryptography;
using System.Text;

namespace ColegioMaster.Application.Utilidades;

public class CifradoAes
{
    private readonly byte[] _clave;
    private readonly byte[] _iv;

    // La clave debe ser de 32 bytes (AES-256) y el IV de 16 bytes
    public CifradoAes(string claveBase64, string ivBase64)
    {
        _clave = Convert.FromBase64String(claveBase64);
        _iv    = Convert.FromBase64String(ivBase64);

        if (_clave.Length != 32)
            throw new ArgumentException("La clave AES debe ser de 32 bytes (AES-256).");
        if (_iv.Length != 16)
            throw new ArgumentException("El IV AES debe ser de 16 bytes.");
    }

    /// <summary>
    /// Cifra el texto plano y retorna el resultado en Base64.
    /// </summary>
    public string Cifrar(string textoCifrar)
    {
        using var aes = Aes.Create();
        aes.Key  = _clave;
        aes.IV   = _iv;
        aes.Mode = CipherMode.CBC;

        using var cifrador  = aes.CreateEncryptor();
        byte[] bytesTexto   = Encoding.UTF8.GetBytes(textoCifrar);
        byte[] bytesCifrado = cifrador.TransformFinalBlock(bytesTexto, 0, bytesTexto.Length);

        return Convert.ToBase64String(bytesCifrado);
    }

    /// <summary>
    /// Descifra un texto en Base64 y retorna el texto original.
    /// </summary>
    public string Descifrar(string textoCifradoBase64)
    {
        using var aes = Aes.Create();
        aes.Key  = _clave;
        aes.IV   = _iv;
        aes.Mode = CipherMode.CBC;

        using var descifrador  = aes.CreateDecryptor();
        byte[] bytesCifrado    = Convert.FromBase64String(textoCifradoBase64);
        byte[] bytesDescifrado = descifrador.TransformFinalBlock(bytesCifrado, 0, bytesCifrado.Length);

        return Encoding.UTF8.GetString(bytesDescifrado);
    }

    /// <summary>
    /// Genera una nueva clave AES-256 aleatoria en Base64 (para configuración inicial).
    /// </summary>
    public static string GenerarClaveBase64()
    {
        var clave = new byte[32];
        RandomNumberGenerator.Fill(clave);
        return Convert.ToBase64String(clave);
    }

    /// <summary>
    /// Genera un nuevo IV AES aleatorio en Base64 (para configuración inicial).
    /// </summary>
    public static string GenerarIvBase64()
    {
        var iv = new byte[16];
        RandomNumberGenerator.Fill(iv);
        return Convert.ToBase64String(iv);
    }
}
