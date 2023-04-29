using Blazor.Interfaces;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Modelos;

namespace Blazor.Pages.Facturacion
{
	public partial class NuevaFactura
	{
		[Inject] private IFacturaServicio facturaServicio { get; set; }
		[Inject] private IDetalleFacturaServicio detalleFacturaServicio { get; set; }
		[Inject] private IClienteServicio clienteServicio { get; set; }
		[Inject] private IProductoServicio productoServicio { get; set; }
		[Inject] private SweetAlertService Swal { get; set; }
		[Inject] NavigationManager _navigationManager { get; set; }
		[Inject] private IHttpContextAccessor httpContextAccessor { get; set; }
		public bool registrado;
		public Factura factura = new Factura();
		private List<DetalleFactura> listaDetalleFactura = new List<DetalleFactura>();
		private Cliente cliente = new Cliente();
		private Producto producto = new Producto();

		private int cantidad { get; set; }
		private string codigoProducto { get; set; }

		private async void BuscarCliente()
		{
			cliente = await clienteServicio.GetPorIdentidadAsync(factura.IdentidadCliente);
		}

		protected override async Task OnInitializedAsync()
		{
			factura.Fecha = DateTime.Now;
		}
		private async void BuscarProducto()
		{
			producto = await productoServicio.GetPorCodigoAsync(codigoProducto);
		}
		protected async Task AgregarProducto(MouseEventArgs args)
		{
			decimal Descuento = factura.Descuento;
			if (args.Detail != 0)
			{
				if (producto != null)
				{
					registrado = false;//actualizando variable  de control para que los productos agregados aparezcan en la tabla o datagrid
					DetalleFactura detalle = new DetalleFactura();
					detalle.Descripcion = producto.Descripcion;
					detalle.CodigoProducto = producto.Codigo;
					detalle.Cantidad = Convert.ToInt32(cantidad);
					detalle.Precio = producto.Precio;
					detalle.Total = producto.Precio * Convert.ToInt32(cantidad);
					listaDetalleFactura.Add(detalle);
					producto.Descripcion = string.Empty;
					producto.Precio = 0;
					producto.Existencia = 0;
					cantidad = 0;
					codigoProducto = "0";

					factura.SubTotal = factura.SubTotal + detalle.Total;
					factura.ISV = factura.SubTotal * 0.15M;
					factura.Total = (factura.SubTotal + factura.ISV) - Descuento;
				}
			}
		}

		protected async Task Guardar()
		{
			factura.CodigoUsuario = httpContextAccessor.HttpContext.User.Identity.Name;
			int idFactura = await facturaServicio.Nueva(factura);
			if (idFactura != 0)
			{
				foreach (var item in listaDetalleFactura)
				{
					item.IdFactura = idFactura;
					await detalleFacturaServicio.Nuevo(item);

				}
				factura.IdentidadCliente = string.Empty;
				cliente.Nombre = string.Empty;
				factura.ISV = 0;
				factura.Descuento = 0;
				factura.SubTotal = 0;
				factura.Total = 0;
				registrado = true;
				await Swal.FireAsync("Realizado", "Factura guardada con éxito", SweetAlertIcon.Success);
			}
			else
			{
				await Swal.FireAsync("Error", "No se pudo guardar la factura", SweetAlertIcon.Error);
			}

		}
		protected async Task Cancelar()
		{

			_navigationManager.NavigateTo("/");
		}
	}
}
