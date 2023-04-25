using Blazor.Interfaces;
using Microsoft.AspNetCore.Components;
using Modelos;

namespace Blazor.Pages.MisClientes
{
    public partial class Clientes
    {
        [Inject] IClienteServicio clienteServicio { get; set; }
        IEnumerable<Cliente> listaClientes { get; set; }

        protected override async Task OnInitializedAsync()
        {
            listaClientes = await clienteServicio.GetListaAsync();
        }
    }
}
