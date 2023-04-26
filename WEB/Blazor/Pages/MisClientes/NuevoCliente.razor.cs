using Blazor.Interfaces;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Modelos;

namespace Blazor.Pages.MisClientes
{
    public partial class NuevoCliente
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

            cliente.FechaCreacion = DateTime.Now;

            Cliente clienExistente = new Cliente();
            clienExistente = await clienteServicio.GetPorIdentidadAsync(cliente.Identidad);

            if (clienExistente == null)
            {
                await Swal.FireAsync("Advertencia", "Ya esiste un cliente con el mismo número de DNI", SweetAlertIcon.Warning);
                return;
            }

            cliente.FechaCreacion = DateTime.Now;

            bool inserto = await clienteServicio.NuevoAsync(cliente);
            if (inserto)
            {
                await Swal.FireAsync("Error", "El cliente no se pudo guardar", SweetAlertIcon.Error);
            }
            else
            {
                await Swal.FireAsync("Éxito", "Cliente guardado exitosamente", SweetAlertIcon.Success);
                navigationManager.NavigateTo("/Clientes");
            }
        }

        protected async Task Cancelar()
        {
            navigationManager.NavigateTo("/Clientes");
        }
    }
}
