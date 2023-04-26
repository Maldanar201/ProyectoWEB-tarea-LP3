using Blazor.Interfaces;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Modelos;

namespace Blazor.Pages.MisUsuarios
{
    public partial class EditarUsuario
    {
        [Inject] private IUsuarioServicio usuariosServicios { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private SweetAlertService sweetAlertService { get; set; }

        private Usuario user = new Usuario();
        [Parameter] public string CodigoUsuario { get; set; }
        string ImgURL = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(CodigoUsuario))
            {
                user = await usuariosServicios.GetPorCodigoAsync(CodigoUsuario);
            }
        }

        private async Task SeleccionarImagen(InputFileChangeEventArgs e)
        {
            IBrowserFile ImgFile = e.File;

            var buffers = new byte[ImgFile.Size];
            user.Foto = buffers;
            await ImgFile.OpenReadStream().ReadAsync(buffers);
            string imgType = ImgFile.ContentType;
            ImgURL = $"data:{imgType}; base64, {Convert.ToBase64String(buffers)}";

        }

        protected async void Guardar()
        {
            if (string.IsNullOrWhiteSpace(user.CodigoUsuario) || string.IsNullOrWhiteSpace(user.Nombre) ||
                string.IsNullOrWhiteSpace(user.Contrasena) || string.IsNullOrWhiteSpace(user.Rol) || user.Rol == "Seleccionar")
            {
                return;
            }

            bool edito = await usuariosServicios.ActualizarAsync(user);

            if (edito)
            {
                await sweetAlertService.FireAsync("Realizado", "Usuario Actualizado", SweetAlertIcon.Success);
                navigationManager.NavigateTo("/Usuarios");
            }
            else
            {
                await sweetAlertService.FireAsync("Error", "No se pudo actualizar el usuario", SweetAlertIcon.Error);
            }

        }

        protected async void Cancelar()
        {
            navigationManager.NavigateTo("/Usuarios");
        }

        protected async void Eliminar()
        {
            SweetAlertResult result = await sweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "¿Seguro que desea eliminar el usuario seleccionado?",
                Text = "Esta acción no se podrá revertir",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
                ConfirmButtonText = "Sí",
                CancelButtonText = "No"
            });

            if (!string.IsNullOrEmpty(result.Value))
            {
                bool elimino = await usuariosServicios.EliminarAsync(user.CodigoUsuario);

                if (elimino)
                {
                    await sweetAlertService.FireAsync("Realizado", "Usuario Eliminado", SweetAlertIcon.Success);
                    navigationManager.NavigateTo("/Usuarios");
                }
                else
                {
                    await sweetAlertService.FireAsync("Error", "No se pudo eliminar el usuario", SweetAlertIcon.Error);
                }
            }
        }

    }
}

enum Roles
{
    Seleccionar,
    Administrador,
    Usuario
}
