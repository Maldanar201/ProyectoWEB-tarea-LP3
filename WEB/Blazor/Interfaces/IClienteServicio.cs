namespace Blazor.Interfaces
{
    public interface IClienteServicio
    {
        Task<Cliente> GetPorIdentidadAsync(string identidad);
        Task<bool> NuevoAsync(Cliente cliente);
        Task<bool> ActualizarAsync(Cliente cliente);
        Task<bool> EliminarAsync(string identidad);
        Task<IEnumerable<Cliente>> GetListaAsync();
    }
}
