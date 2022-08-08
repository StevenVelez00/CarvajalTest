using Carvajal.Shared.Entidades;

namespace Carvajal.Client.Services.Productos
{
    public interface IProductoService
    {
        List<Producto> Productos { get; set; }
        Task ObtenerProductos();
        Task<Producto> ObtenerProductoID(int id);
        Task CrearProducto(Producto producto);
        Task ActualizarProducto(Producto producto);
        Task EliminarProducto(int id);
    }
}
