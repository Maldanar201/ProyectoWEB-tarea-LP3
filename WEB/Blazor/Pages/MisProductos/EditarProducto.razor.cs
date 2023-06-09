﻿using Blazor.Interfaces;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Modelos;

namespace Blazor.Pages.MisProductos
{
    public partial class EditarProducto
    {
        [Inject] private IProductoServicio productosServicios { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private SweetAlertService sweetAlertService { get; set; }

        private Producto prod = new Producto();
        [Parameter] public string Codigo { get; set; }
        string ImgURL = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(Codigo))
            {
                prod = await productosServicios.GetPorCodigoAsync(Codigo);
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

            bool edito = await productosServicios.ActualizarAsync(prod);

            if (edito)
            {
                await sweetAlertService.FireAsync("Realizado", "Producto Actualizado", SweetAlertIcon.Success);
                navigationManager.NavigateTo("/Productos");
            }
            else
            {
                await sweetAlertService.FireAsync("Error", "No se pudo actualizar el producto", SweetAlertIcon.Error);
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
                Title = "¿Seguro que desea eliminar el producto seleccionado?",
                Text = "Esta acción no se podrá revertir",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
                ConfirmButtonText = "Sí",
                CancelButtonText = "No"
            });

            if (!string.IsNullOrEmpty(result.Value))
            {
                bool elimino = await productosServicios.EliminarAsync(prod.Codigo);

                if (elimino)
                {
                    await sweetAlertService.FireAsync("Realizado", "El producto ha sido eliminado", SweetAlertIcon.Success);
                    navigationManager.NavigateTo("/Productos");
                }
                else
                {
                    await sweetAlertService.FireAsync("Error", "No se Pudo eliminar el producto", SweetAlertIcon.Error);
                }
            }
        }
    }
}
