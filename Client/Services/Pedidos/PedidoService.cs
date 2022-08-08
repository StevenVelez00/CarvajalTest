using Carvajal.Shared.Entidades;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Carvajal.Client.Services.Productos
{
    public class PedidoService : IPedidoService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public PedidoService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public List<Pedido> listPedidos { get; set; } = new List<Pedido>();

        public List<Usuario> listUsuarios { get; set; } = new List<Usuario>();

        public List<Producto> listProductos { get; set; } = new List<Producto>();


        //pedidos
        public async Task ObtenerPedidos(string queryStrings)
        {
            var result = await _http.GetFromJsonAsync<List<Pedido>>($"api/pedido?{queryStrings}" );

            if (result != null)
                listPedidos = result;
        }

        public async Task<Pedido> ObtenerPedidoID(int id)
        {
            var result = await _http.GetFromJsonAsync<Pedido>($"api/pedido/{id}");

            if (result != null)
                return result;

            throw new Exception("No existe el Pedido");
        }

        public async Task CrearPedido(Pedido pedido)
        {
            var result = await _http.PostAsJsonAsync("api/pedido", pedido);
            var ped = await _http.GetFromJsonAsync<Pedido>($"api/pedido/ultimoId");

            _navigationManager.NavigateTo($"pedCrud/{ped.PedId}");
        }

        public async Task ActualizarPedido(Pedido pedido)
        {
            var result = await _http.PutAsJsonAsync($"api/pedido/{pedido.PedId}", pedido);
            _navigationManager.NavigateTo($"pedCrud/{pedido.PedId}");
            //await SetPedidos(result);
        }

        public async Task EliminarPedido(int id)
        {
            var result = await _http.DeleteAsync($"api/pedido/{id}");
            await SetPedidos(result);
        }

        private async Task SetPedidos(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Pedido>>();

            listPedidos = response;

            _navigationManager.NavigateTo("pedidos");
        }

        //Usuarios
        public async Task ObtenerUsuarios()
        {
            var result = await _http.GetFromJsonAsync<List<Usuario>>("api/usuario");
            
            if (result != null)
                listUsuarios = result;
        }

        //Productos
        public async Task ObtenerProductos()
        {
            var result = await _http.GetFromJsonAsync<List<Producto>>("api/producto");

            if (result != null)
                listProductos = result;
        }
    }
}
