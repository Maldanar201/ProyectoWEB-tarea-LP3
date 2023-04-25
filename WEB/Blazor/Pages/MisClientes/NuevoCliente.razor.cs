using Blazor.Interfaces;
using Blazor.Servicios;
using CurrieTechnologies.Razor.SweetAlert2;
using Google.Protobuf.WellKnownTypes;
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
                await Swal.FireAsync("Advertencia", "Ya esiste un Cliente con el mismo numero de Identidad", SweetAlertIcon.Warning);
                return;
            }

            cliente.FechaCreacion = DateTime.Now;

            bool inserto = await clienteServicio.NuevoAsync(cliente);
            if (inserto)
            {
                await Swal.FireAsync("Error", "El Cliente no se pudo Guardar", SweetAlertIcon.Error);
            }
            else
            {                
                await Swal.FireAsync("Atencion", "Cliente Guardado exitosamente", SweetAlertIcon.Success);
                navigationManager.NavigateTo("/Clientes");
            }
        }

        protected async Task Cancelar()
        {
            navigationManager.NavigateTo("/Clientes");
        }
    }
}
