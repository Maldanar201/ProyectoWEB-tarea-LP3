using Dapper;
using Datos.Interfaces;
using Modelos;
using MySql.Data.MySqlClient;

namespace Datos.Repositorios
{
    public class FacturaRepositorio : IFacturaRepositorio
    {
        private String CadenaConexion;
        public FacturaRepositorio(string _cadenaConexion)
        {
            CadenaConexion = _cadenaConexion;
        }

        private MySqlConnection Conexion()
        {
            return new MySqlConnection(CadenaConexion);
        }
        public async Task<int> Nueva(Factura factura)
        {
            int idFactura = 0;
            try
            {
                using MySqlConnection conexion = Conexion();
                await conexion.OpenAsync();
                string sql = @"INSERT INTO factura (Fecha, IdentidadCliente, CodigoUsuario, ISV, Descuento, SubTotal, Total) 
                               VALUES (@Fecha, @IdentidadCliente, @CodigoUsuario, @ISV, @Descuento, @SubTotal, @Total); SELECT LAST_INSERT_ID()";
                idFactura = Convert.ToInt32(await conexion.ExecuteScalarAsync(sql, factura));
            }
            catch (Exception)
            {
            }
            return idFactura;
        }
    }
}
