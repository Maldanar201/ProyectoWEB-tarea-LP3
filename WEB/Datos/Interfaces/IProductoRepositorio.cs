using Modelos;

namespace Datos.Interfaces
{
    public interface IProductoRepositorio
    {
        Task<Producto> GetPorCodigoAsync(string codigo);
        Task<bool> NuevoAsync(Producto producto);
        Task<bool> ActualizarAsync(Producto producto);
        Task<bool> EliminarAsync(string codigo);
        Task<IEnumerable<Producto>> GetListaAsync();
    }
}
