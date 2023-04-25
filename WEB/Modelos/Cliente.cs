using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Cliente
    {
        [Required(ErrorMessage = "El numero de identidad es Requerido")]
        public string Identidad { get; set; }

        [Required(ErrorMessage = "El nombre es Requerido")]
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool EstaActivo { get; set; }

        public Cliente()
        {

        }
        public Cliente(string identidad, string nombre, string telefono, string correo, string direccion, DateTime fechaNacimiento, bool estaActivo)
        {
            Identidad = identidad;
            Nombre = nombre;
            Telefono = telefono;
            Correo = correo;
            Direccion = direccion;
            FechaNacimiento = fechaNacimiento;
            EstaActivo = estaActivo;
        }
    }
}
