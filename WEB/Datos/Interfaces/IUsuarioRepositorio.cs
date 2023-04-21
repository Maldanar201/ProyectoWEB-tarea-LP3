using Modelos;

namespace Datos.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario> GetPorCodigoAsync(string codigoUsuario);
    }
}
