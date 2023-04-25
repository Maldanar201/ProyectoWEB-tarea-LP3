using Blazor.Interfaces;
using Blazor.Servicios;
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

        Cliente cliente = new Cliente();

        [Parameter] public string Identidad { get; set; }

        protected async Task Guardar()
        {
            if (string.IsNullOrWhiteSpace(cliente.Identidad) || string.IsNullOrWhiteSpace(cliente.Nombre))
            {
                return;
            }

            bool edito = await clienteServicio.NuevoAsync(cliente);
            if (edito)
            {
                await Swal.FireAsync("Atencion", "Cliente Guardado exitosamente", SweetAlertIcon.Success);
            }
            else
            {
                await Swal.FireAsync("Error", "El Cliente no se pudo Guardar", SweetAlertIcon.Error);
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
                Title = "¿Seguro que desea eliminar el Cliente?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
                ConfirmButtonText = "Aceptar",
                CancelButtonText = "Cancelar",
            });

            if (!string.IsNullOrEmpty(result.Value))
            {
                elimino = await clienteServicio.EliminarAsync(cliente.Identidad);

                if (elimino)
                {
                    await Swal.FireAsync("Felicidades", "El Cliente se Elimino", SweetAlertIcon.Success);
                    navigationManager.NavigateTo("/Clientes");
                }
                else
                {
                    await Swal.FireAsync("Error", "El Cliente No pudo ser Eliminado", SweetAlertIcon.Error);
                }
            }

            navigationManager.NavigateTo("/Clientes");
        }
    }
}

