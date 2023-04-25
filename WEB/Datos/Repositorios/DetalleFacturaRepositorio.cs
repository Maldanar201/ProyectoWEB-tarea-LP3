using Dapper;
using Datos.Interfaces;
using Modelos;
using MySql.Data.MySqlClient;

namespace Datos.Repositorios
{
    public class DetalleFacturaRepositorio : IDetalleFacturaRepositorio
    {
        private string CadenaConexon;

        public DetalleFacturaRepositorio(string _cadenaConexion)
        {
            CadenaConexon = _cadenaConexion;
        }

        private MySqlConnection Conexion()
        {
            return new MySqlConnection(CadenaConexon);
        }

        public async Task<bool> Nuevo(DetalleFactura detalle)
        {
            bool ingreso = false;
            try
            {
                using MySqlConnection conexion = Conexion();
                await conexion.OpenAsync();
                string sql = @"INSERT INTO detallefactura (IdFactura, CodigoProducto, Precio, Cantidad, Total) 
                               VALUES (@IdFactura, @CodigoProducto, @Precio, @Cantidad, @Total);";
                ingreso = Convert.ToBoolean(await conexion.ExecuteAsync(sql, detalle));
            }
            catch (Exception)//revisar posible variable ex
            {
            }
            return ingreso;
        }
    }
}
