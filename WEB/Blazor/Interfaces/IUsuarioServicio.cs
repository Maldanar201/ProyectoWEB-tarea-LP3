using Modelos;

namespace Blazor.Interfaces
{
    public interface IUsuarioServicio
    {
        Task<Usuario> GetPorCodigoAsync(string codigoUsuario);
        Task<bool> NuevoAsync(Usuario usuario);
        Task<bool> ActualizarAsync(Usuario usuario);
        Task<bool> EliminarAsync(string usuarioCodigo);
        Task<IEnumerable<Usuario>> GetListaAsync();
    }
}
