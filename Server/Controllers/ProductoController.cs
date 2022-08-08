using Carvajal.Server.Data;
using Carvajal.Shared.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Carvajal.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductoController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ProductoController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> ObtenerProductos()
        {
            return await context.Productos.OrderBy(p => p.ProDesc).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> ObtenerProductoID(int id)
        {
            var producto = await context.Productos.FirstOrDefaultAsync(x => x.ProID == id);

            if (producto == null) { return NotFound(); }

            return producto;
        }

        [HttpPost]
        public async Task<ActionResult<List<Producto>>> CrearProducto(Producto producto)
        {
            context.Productos.Add(producto);
            await context.SaveChangesAsync();

            return Ok(await ObtenerDbProductos());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Producto>>> ActualizarProducto(Producto producto, int id)
        {
            var dbProducto = await context.Productos
                .FirstOrDefaultAsync(p => p.ProID == id);

            if (dbProducto == null)
                return NotFound("Lo sentimos, no existe el producto");

            dbProducto.ProDesc = producto.ProDesc;
            dbProducto.ProValor = producto.ProValor;

            await context.SaveChangesAsync();

            return Ok(await ObtenerDbProductos());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Producto>>> EliminarProducto(int id)
        {
            var dbProducto = await context.Productos
                .FirstOrDefaultAsync(p => p.ProID == id);

            if (dbProducto == null)
                return NotFound("Lo sentimos, no existe el producto");

            context.Productos.Remove(dbProducto);
            await context.SaveChangesAsync();

            return Ok(await ObtenerDbProductos());
        }

        private async Task<List<Producto>> ObtenerDbProductos()
        {
            return await context.Productos.ToListAsync();
        }
    }
}
