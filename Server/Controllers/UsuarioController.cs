using Carvajal.Server.Data;
using Carvajal.Server.Models;
using Carvajal.Shared.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Carvajal.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public UsuarioController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            return await context.Usuarios.Select(p => new Usuario
            {
                UsuID = p.UsuID,
                UsuNombre = p.UsuNombre,
                UsuPass = p.UsuPass,
                UsuPassDecrypt = Crypto.DecryptString(p.UsuPass).ToString()

            }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.UsuID == id);

            if (usuario == null) { return NotFound(); }

            return usuario;
        }

        [HttpPost]
        public async Task<ActionResult<List<Usuario>>> CreateUsuario(Usuario usuario)
        {
            usuario.UsuPass = Crypto.EncryptString(usuario.UsuPass).ToString();

            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();

            return Ok(await GetDbUsuarios());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Usuario>>> UpdateUsuario(Usuario usuario, int id)
        {
            var dbUsuario = await context.Usuarios.FirstOrDefaultAsync(p => p.UsuID == id);

            if (dbUsuario == null)
                return NotFound("Lo sentimos, no existe el usuario");

            dbUsuario.UsuNombre = usuario.UsuNombre;
            dbUsuario.UsuPass = Crypto.EncryptString(usuario.UsuPass).ToString();

            await context.SaveChangesAsync();

            return Ok(await GetDbUsuarios());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Usuario>>> DeleteUsuario(int id)
        {
            var dbUsuario = await context.Usuarios
                .FirstOrDefaultAsync(p => p.UsuID == id);

            if (dbUsuario == null)
                return NotFound("Lo sentimos, no existe el usuario");

            context.Usuarios.Remove(dbUsuario);
            await context.SaveChangesAsync();

            return Ok(await GetDbUsuarios());
        }

        private async Task<List<Usuario>> GetDbUsuarios()
        {
            return await context.Usuarios.ToListAsync();
        }
    }
}
