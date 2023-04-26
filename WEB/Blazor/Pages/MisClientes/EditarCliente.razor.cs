using Blazor.Interfaces;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Modelos;

namespace Blazor.Pages.MisClientes
{
    public partial class EditarCliente
    {
        [Inject] private IClienteServicio clienteServicio { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private SweetAlertService Swal { get; set; }

        private Cliente cliente = new Cliente();

        [Parameter] public string Identidad { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(Identidad))
            {
                cliente = await clienteServicio.GetPorIdentidadAsync(Identidad);
            }
        }

        protected async Task Guardar()
        {
            if (string.IsNullOrWhiteSpace(cliente.Identidad) || string.IsNullOrWhiteSpace(cliente.Nombre))
            {
                return;
            }

            bool edito = await clienteServicio.ActualizarAsync(cliente);
            if (edito)
            {
                await Swal.FireAsync("Realizado", "Cliente guardado exitosamente", SweetAlertIcon.Success);
            }
            else
            {
                await Swal.FireAsync("Error", "El cliente no se pudo guardar", SweetAlertIcon.Error);
            }
        }

        protected async Task Cancelar()
        {
            navigationManager.NavigateTo("/Clientes");
        }

        protected async Task Eliminar()
        {
            bool elimino = false;

            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = "¿Seguro que desea eliminar el Cliente seleccionado?",
                Text = "Esta acción no se podrá revertir",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
                ConfirmButtonText = "Sí",
                CancelButtonText = "No",
            });

            if (!string.IsNullOrEmpty(result.Value))
            {
                elimino = await clienteServicio.EliminarAsync(cliente.Identidad);

                if (elimino)
                {
                    await Swal.FireAsync("Realizado", "El Cliente ha sido eliminado", SweetAlertIcon.Success);
                    navigationManager.NavigateTo("/Clientes");
                }
                else
                {
                    await Swal.FireAsync("Error", "No se pudo eliminar el cliente", SweetAlertIcon.Error);
                }
            }

            navigationManager.NavigateTo("/Clientes");
        }
    }
}

