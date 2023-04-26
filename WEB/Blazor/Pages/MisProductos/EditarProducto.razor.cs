using Blazor.Interfaces;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Modelos;

namespace Blazor.Pages.MisProductos
{
    public partial class EditarProducto
    {
        [Inject] private IProductoServicio productosServicvios { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private SweetAlertService sweetAlertService { get; set; }

        private Producto prod = new Producto();
        [Parameter] public string Codigo { get; set; }
        string ImgURL = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(Codigo))
            {
                prod = await productosServicvios.GetPorCodigoAsync(Codigo);
            }
        }

        private async Task SeleccionarImagen(InputFileChangeEventArgs e)
        {
            IBrowserFile ImgFile = e.File;

            var buffers = new byte[ImgFile.Size];
            prod.Foto = buffers;
            await ImgFile.OpenReadStream().ReadAsync(buffers);
            string imgType = ImgFile.ContentType;
            ImgURL = $"data:{imgType}; base64, {Convert.ToBase64String(buffers)}";
        }

        protected async void Guardar()
        {
            if (string.IsNullOrWhiteSpace(prod.Codigo) || string.IsNullOrWhiteSpace(prod.Descripcion))
            {
                return;
            }

            bool edito = await productosServicvios.ActualizarAsync(prod);

            if (edito)
            {
                await sweetAlertService.FireAsync("Felicidades", "Producto Actualizado", SweetAlertIcon.Success);
                navigationManager.NavigateTo("/Productos");
            }
            else
            {
                await sweetAlertService.FireAsync("Error", "No Se Pudo Actualizar El Producto", SweetAlertIcon.Error);
            }
        }
        protected async void Cancelar()
        {
            navigationManager.NavigateTo("/Productos");
        }

        protected async void Eliminar()
        {
            SweetAlertResult result = await sweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "¿Seguro que Desea Eliminar el Producto?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
                ConfirmButtonText = "Aceptar",
                CancelButtonText = "Cancelar"
            });

            if (!string.IsNullOrEmpty(result.Value))
            {
                bool elimino = await productosServicvios.EliminarAsync(prod.Codigo);

                if (elimino)
                {
                    await sweetAlertService.FireAsync("Felicidades", "Producto Eliminado", SweetAlertIcon.Success);
                    navigationManager.NavigateTo("/Productos");
                }
                else
                {
                    await sweetAlertService.FireAsync("Error", "No Se Pudo Eliminar El Producto", SweetAlertIcon.Error);
                }
            }
        }
    }
}
