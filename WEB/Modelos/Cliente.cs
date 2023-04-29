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
        public DateTime FechaCreacion { get; set; }
        public bool EstaActivo { get; set; }       
    }
}
