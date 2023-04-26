using Dapper;
using Datos.Interfaces;
using Modelos;
using MySql.Data.MySqlClient;

namespace Datos.Repositorios
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        private string CadenaConexion;

        public ProductoRepositorio(string cadenaConexion)
        {
            CadenaConexion = cadenaConexion;
        }

        private MySqlConnection Conexion()
        {
            return new MySqlConnection(CadenaConexion);
        }

        public async Task<bool> ActualizarAsync(Producto producto)
        {
            bool resultado = false;

            try
            {
                using MySqlConnection _conexion = Conexion();
                await _conexion.OpenAsync();
                string sql = @"UPDATE producto SET Descripcion = @Descripcion, Existencia = @Existencia, Precio = @Precio, Foto = @Foto, EstaActivo = @EstaActivo
                             WHERE Codigo = @Codigo; ";
                resultado = Convert.ToBoolean(await _conexion.ExecuteScalarAsync(sql, producto));
                resultado = true;
            }
            catch (Exception)
            {
            }

            return resultado;

        }

        public async Task<bool> EliminarAsync(string codigo)
        {
            bool resultado = false;

            try
            {
                using MySqlConnection _conexion = Conexion();
                await _conexion.OpenAsync();
                string sql = "DELETE FROM producto WHERE Codigo = @Codigo; ";
                resultado = Convert.ToBoolean(await _conexion.ExecuteScalarAsync(sql, new { codigo }));
                resultado = true;
            }
            catch (Exception)
            {
            }

            return resultado;

        }

        public async Task<IEnumerable<Producto>> GetListaAsync()
        {
            IEnumerable<Producto> lista = new List<Producto>();

            try
            {
                using MySqlConnection _conexion = Conexion();
                await _conexion.OpenAsync();
                string sql = "SELECT * FROM producto; ";
                lista = await _conexion.QueryAsync<Producto>(sql);

            }
            catch (Exception)
            {
            }

            return lista;

        }

        public async Task<Producto> GetPorCodigoAsync(string codigo)
        {
            Producto prod = new Producto();

            try
            {
                using MySqlConnection _conexion = Conexion();
                await _conexion.OpenAsync();
                string sql = "SELECT * FROM producto WHERE Codigo = @Codigo; ";
                prod = await _conexion.QueryFirstAsync<Producto>(sql, new { codigo });

            }
            catch (Exception)
            {
            }

            return prod;

        }

        public async Task<bool> NuevoAsync(Producto producto)
        {
            bool resultado = false;

            try
            {
                using MySqlConnection _conexion = Conexion();
                await _conexion.OpenAsync();
                string sql = @"INSERT INTO producto (Codigo, Descripcion, Existencia, Precio, Foto, EstaActivo)
                             Values (@Codigo, @Descripcion, @Existencia, @Precio, @Foto, @EstaActivo); ";
                resultado = Convert.ToBoolean(await _conexion.ExecuteScalarAsync(sql, producto));
                resultado = true;
            }
            catch (Exception)
            {
            }

            return resultado;

        }

    }
}


