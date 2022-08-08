using Carvajal.Server.Data;
using Carvajal.Shared.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Carvajal.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PedidoController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public PedidoController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pedido>>> ObtenerPedido([FromQuery] ParametrosBusquedaPedidos parametrosBusqueda)
        {
            var queryable = context.Pedidos.Include(u => u.Usuario).Include(p => p.Producto).AsQueryable();

            if (parametrosBusqueda.UsuId.ToString() != "-1")
            {
                queryable = queryable.Where(x => x.UsuarioUsuID == parametrosBusqueda.UsuId);
            }

            if (parametrosBusqueda.ProId.ToString() != "-1")
            {
                queryable = queryable.Where(x => x.ProductoProID == parametrosBusqueda.ProId);
            }

            return await queryable.ToListAsync();
        }

        [HttpGet("ultimoId")]
        public async Task<ActionResult<Pedido>> ObtenerUltimoPedidoId()
        {
            return await context.Pedidos.OrderByDescending(p => p.PedId).FirstOrDefaultAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> ObtenerPedidoID(int id)
        {
            var pedidos = await context.Pedidos
                .FirstOrDefaultAsync(x => x.PedId == id);

            if (pedidos == null) { return NotFound(); }

            return pedidos;
        }

        [HttpPost]
        public async Task<ActionResult<List<Pedido>>> CrearPedido(Pedido pedido)
        {
            context.Pedidos.Add(pedido);
            await context.SaveChangesAsync();

            return Ok(await ObtenerDbPedidos());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Pedido>>> ActualizarPedido(Pedido pedido, int id)
        {
            var dbPedido = await context.Pedidos
                .FirstOrDefaultAsync(p => p.PedId == id);
            
            if (dbPedido == null)
                return NotFound("Lo sentimos, no existe el pedido");

            dbPedido.Producto = pedido.Producto;
            dbPedido.Usuario = pedido.Usuario;
            dbPedido.pedVrUnit = pedido.pedVrUnit;
            dbPedido.PedCant = pedido.PedCant;
            dbPedido.PedSubtot = pedido.PedSubtot;
            dbPedido.PedIVA = pedido.PedIVA;
            dbPedido.PedTotal = pedido.PedTotal;
            dbPedido.ProductoProID = pedido.ProductoProID;
            dbPedido.UsuarioUsuID = pedido.UsuarioUsuID;

            await context.SaveChangesAsync();

            return Ok(await ObtenerDbPedidos());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Pedido>>> EliminarPedido(int id)
        {
            var dbPedido = await context.Pedidos
                .FirstOrDefaultAsync(p => p.PedId == id);

            if (dbPedido == null)
                return NotFound("Lo sentimos, no existe el pedido");

            context.Pedidos.Remove(dbPedido);
            await context.SaveChangesAsync();

            return Ok(await ObtenerDbPedidos());
        }

        private async Task<List<Pedido>> ObtenerDbPedidos()
        {
            return await context.Pedidos.ToListAsync();
        }

        public class ParametrosBusquedaPedidos
        {
            public int UsuId { get; set; }
            public int ProId { get; set; }
        }
    }
}
