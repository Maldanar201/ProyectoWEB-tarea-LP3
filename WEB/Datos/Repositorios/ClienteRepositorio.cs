using Dapper;
using Datos.Interfaces;
using Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private string CadenaConexion;

        public ClienteRepositorio(string _cadenaConexion)
        {
            CadenaConexion = _cadenaConexion;
        }

        private MySqlConnection Conexion()
        {
            return new MySqlConnection(CadenaConexion);
        }

        public async Task<bool> ActualizarAsync(Cliente cliente)
        {
            bool resultado = false;

            try
            {
                using MySqlConnection _conexion = Conexion();
                await _conexion.OpenAsync();
                string sql = @"UPDATE cliente SET Nombre = @Nombre, Telefono = @Telefono, Correo = @Correo, Direccion = @Direccion, FechaNacimiento = @FechaNacimiento, EstaActivo = @EstaActivo
                             WHERE Identidad = @Identidad; ";
                resultado = Convert.ToBoolean(await _conexion.ExecuteScalarAsync(sql, cliente));
                resultado = true;
            }
            catch (Exception)
            {
            }

            return resultado;
        }

        public async Task<bool> EliminarAsync(string identidad)
        {
            bool resultado = false;

            try
            {
                using MySqlConnection _conexion = Conexion();
                await _conexion.OpenAsync();
                string sql = "DELETE FROM cliente WHERE Identidad = @Identidad; ";
                resultado = Convert.ToBoolean(await _conexion.ExecuteScalarAsync(sql, new { identidad }));
                resultado = true;
            }
            catch (Exception)
            {
            }

            return resultado;
        }

        public async Task<IEnumerable<Cliente>> GetListaAsync()
        {
            IEnumerable<Cliente> lista = new List<Cliente>();

            try
            {
                using MySqlConnection _conexion = Conexion();
                await _conexion.OpenAsync();
                string sql = "SELECT * FROM cliente; ";
                lista = await _conexion.QueryAsync<Cliente>(sql);

            }
            catch (Exception)
            {
            }

            return lista;
        }

        public async Task<Cliente> GetPorIdentidadAsync(string identidad)
        {
            Cliente cliente = new Cliente();

            try
            {
                using MySqlConnection _conexion = Conexion();
                await _conexion.OpenAsync();
                string sql = "SELECT * FROM cliente WHERE Identidad = @Identidad; ";
                cliente = await _conexion.QueryFirstAsync<Cliente>(sql, new { identidad });

            }
            catch (Exception)
            {
            }

            return cliente;
        }

        public async Task<bool> NuevoAsync(Cliente cliente)
        {
            bool resultado = false;

            try
            {
                using MySqlConnection _conexion = Conexion();
                await _conexion.OpenAsync();
                string sql = @"INSERT INTO cliente (Identidad, Nombre, Telefono, Correo, Direccion, FechaNacimiento, EstaActivo)
                             Values (@Identidad, @Nombre, @Telefono, @Correo, @Direccion, @FechaNacimiento, @EstaActivo); ";
                resultado = Convert.ToBoolean(await _conexion.ExecuteScalarAsync(sql, cliente));
                resultado = true;
            }
            catch (Exception)
            {
            }

            return resultado;
        }
    }
}
