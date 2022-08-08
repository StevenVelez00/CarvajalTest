using Carvajal.Shared.Entidades;

namespace Carvajal.Client.Services.Usuarios
{
    public interface IUsuarioService
    {
        List<Usuario> Usuarios { get; set; }
        Task GetUsuarios();
        Task<Usuario> GetUsuarioById(int id);
        Task CreateUsuario(Usuario usuario);
        Task UpdateUsuario(Usuario usuario);
        Task DeleteUsuario(int id);
    }
}
