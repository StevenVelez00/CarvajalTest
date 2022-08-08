using Carvajal.Shared.Entidades;

namespace Carvajal.Client.Services.Productos
{
    public interface IPedidoService
    {
        List<Pedido> listPedidos { get; set; }
        List<Usuario> listUsuarios { get; set; }
        List<Producto> listProductos { get; set; }

        //Pedidos
        Task ObtenerPedidos(string queryStrings);
        Task<Pedido> ObtenerPedidoID(int id);
        Task CrearPedido(Pedido producto);
        Task ActualizarPedido(Pedido producto);
        Task EliminarPedido(int id);

        //Usuarios
        Task ObtenerUsuarios();

        //Productos
        Task ObtenerProductos();
    }
}
