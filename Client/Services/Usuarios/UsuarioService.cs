using Carvajal.Shared.Entidades;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Carvajal.Client.Services.Usuarios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public UsuarioService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();

        public async Task GetUsuarios()
        {
            var result = await _http.GetFromJsonAsync<List<Usuario>>("api/usuario");

            if (result != null)
                Usuarios = result;
        }

        public async Task<Usuario> GetUsuarioById(int id)
        {
            var result = await _http.GetFromJsonAsync<Usuario>($"api/usuario/{id}");

            if (result != null)
                return result;

            throw new Exception("No existe el usuario");
        }

        public async Task CreateUsuario(Usuario usuario)
        {
            var result = await _http.PostAsJsonAsync("api/usuario", usuario);
            await SetUsuarios(result);
        }

        public async Task UpdateUsuario(Usuario usuario)
        {
            var result = await _http.PutAsJsonAsync($"api/usuario/{usuario.UsuID}", usuario);
            await SetUsuarios(result);
        }

        public async Task DeleteUsuario(int id)
        {
            var result = await _http.DeleteAsync($"api/usuario/{id}");
            await SetUsuarios(result);
        }

        private async Task SetUsuarios(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Usuario>>();

            Usuarios = response;

            _navigationManager.NavigateTo("usuarios");
        }
    }
}
