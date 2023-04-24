using System.ComponentModel.DataAnnotations;

namespace Modelos
{
    public class Usuario
    {
        [Required(ErrorMessage = "El Codigo es Requerido")]
        public string CodigoUsuario { get; set; }
        [Required(ErrorMessage = "El Nombre es Requerido")]
        public string Nombre { get; set; }
        public string Contrasena { get; set; }
        public string Correo { get; set; }
        [Required(ErrorMessage = "El Rol es Requerido")]
        public string Rol { get; set; }
        public byte[] Foto { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool EstaActivo { get; set; }

        public Usuario()
        {
        }

        public Usuario(string codigoUsuario, string nombre, string contrasena, string correo, string rol, byte[] foto,
            DateTime fechaCreacion, bool estaActivo)
        {
            CodigoUsuario = codigoUsuario;
            Nombre = nombre;
            Contrasena = contrasena;
            Correo = correo;
            Rol = rol;
            Foto = foto;
            FechaCreacion = fechaCreacion;
            EstaActivo = estaActivo;
        }
    }
}
