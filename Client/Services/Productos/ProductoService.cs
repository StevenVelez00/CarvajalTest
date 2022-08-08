using Carvajal.Shared.Entidades;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Carvajal.Client.Services.Productos
{
    public class ProductoService : IProductoService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public ProductoService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public List<Producto> Productos { get; set; } = new List<Producto>();

        public async Task ObtenerProductos()
        {
            var result = await _http.GetFromJsonAsync<List<Producto>>("api/producto");

            if (result != null)
                Productos = result;
        }

        public async Task<Producto> ObtenerProductoID(int id)
        {
            var result = await _http.GetFromJsonAsync<Producto>($"api/producto/{id}");

            if (result != null)
                return result;

            throw new Exception("No existe el Producto");
        }

        public async Task CrearProducto(Producto producto)
        {
            var result = await _http.PostAsJsonAsync("api/producto", producto);
            await SetProductos(result);
        }

        public async Task ActualizarProducto(Producto producto)
        {
            var result = await _http.PutAsJsonAsync($"api/producto/{producto.ProID}", producto);
            await SetProductos(result);
        }

        public async Task EliminarProducto(int id)
        {
            var result = await _http.DeleteAsync($"api/producto/{id}");
            await SetProductos(result);
        }

        private async Task SetProductos(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Producto>>();

            Productos = response;

            _navigationManager.NavigateTo("productos");
        }
    }
}
