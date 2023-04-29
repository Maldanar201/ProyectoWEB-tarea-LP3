using Blazor.Interfaces;
using Datos.Interfaces;
using Datos.Repositorios;
using Modelos;


namespace Blazor.Servicios
{
    public class ProductoServicio : IProductoServicio
    {
        private readonly Config _config;
        private IProductoRepositorio productoRepositorios;

        public ProductoServicio(Config config)
        {
            _config = config;
            productoRepositorios = new ProductoRepositorio(config.CadenaConexion);
        }

        public async Task<bool> ActualizarAsync(Producto producto)
        {
            return await productoRepositorios.ActualizarAsync(producto);
        }

        public async Task<bool> EliminarAsync(string codigo)
        {
            return await productoRepositorios.EliminarAsync(codigo);
        }
        

        public async Task<IEnumerable<Producto>> GetListaAsync()
        {
            return await productoRepositorios.GetListaAsync();
        }        

        public async Task<Producto> GetPorCodigoAsync(string codigo)
        {
            return await productoRepositorios.GetPorCodigoAsync(codigo);
        }

        public async Task<bool> NuevoAsync(Producto producto)
        {
            return await productoRepositorios.NuevoAsync(producto);
        }

    }
}
