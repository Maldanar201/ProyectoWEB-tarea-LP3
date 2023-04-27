using System.ComponentModel.DataAnnotations;

namespace Modelos
{
	public class Producto
	{
		[Required(ErrorMessage = "El Codigo es Obligatorio")]
		public string Codigo { get; set; }
		[Required(ErrorMessage = "La Descripcion es Obligatoria")]
		public string Descripcion { get; set; }
		public string Existencia { get; set; }
		public string Precio { get; set; }
		public byte[] Foto { get; set; }
		public bool EstaActivo { get; set; }

		public Producto()
		{
		}

		public Producto(string codigo, string descripcion, string existencia, string precio, byte[] foto, bool estaActivo)
		{
			Codigo = codigo;
			Descripcion = descripcion;
			Existencia = existencia;
			Precio = precio;
			Foto = foto;
			EstaActivo = estaActivo;
		}
	}
}
