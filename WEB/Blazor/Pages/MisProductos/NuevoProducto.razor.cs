using Blazor.Interfaces;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Modelos;

namespace Blazor.Pages.MisProductos
{
    public partial class NuevoProducto
    {
        [Inject] private IProductoServicio productosServicios { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private SweetAlertService sweetAlertService { get; set; }

        private Producto prod = new Producto();

        string ImgURL = string.Empty;

        private async Task SeleccionarImagen(InputFileChangeEventArgs e)
        {
            IBrowserFile ImgFile = e.File;

            var buffers = new byte[ImgFile.Size];
            prod.Foto = buffers;
            await ImgFile.OpenReadStream().ReadAsync(buffers);
            string imgType = ImgFile.ContentType;
            ImgURL = $"data:{imgType}; base64, {Convert.ToBase64String(buffers)}";

        }

        protected async Task Guardar()
        {
            if (string.IsNullOrWhiteSpace(prod.Codigo) || string.IsNullOrWhiteSpace(prod.Descripcion))
            {
                return;
            }

            Producto prodExistente = new Producto();

            prodExistente = await productosServicios.GetPorCodigoAsync(prod.Codigo);

            if (prodExistente != null)
            {
                if (!string.IsNullOrEmpty(prodExistente.Codigo))
                {
                    await sweetAlertService.FireAsync("Advertencia", "Ya existe un producto con el mismo código", SweetAlertIcon.Warning);
                    return;
                }
            }

            bool inserto = await productosServicios.NuevoAsync(prod);

            if (inserto)
            {
                await sweetAlertService.FireAsync("Éxito", "Producto Guardado", SweetAlertIcon.Success);
                navigationManager.NavigateTo("/Productos");
            }
            else
            {
                await sweetAlertService.FireAsync("Error", "No se pudo guardar el producto", SweetAlertIcon.Error);
            }
        }
        protected async void Cancelar()
        {
            navigationManager.NavigateTo("/Productos");
        }
    }
}
