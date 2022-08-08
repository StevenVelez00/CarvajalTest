using Carvajal.Server.Models;
using Carvajal.Shared.Entidades;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Carvajal.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ////Usuarios
            //modelBuilder.Entity<Usuario>().HasKey(b => b.UsuID);
            //modelBuilder.Entity<Usuario>().Property(b => b.UsuID).ValueGeneratedOnAdd();
            //modelBuilder.Entity<Usuario>().Property(b => b.UsuNombre).HasMaxLength(50).IsRequired();
            //modelBuilder.Entity<Usuario>().Property(b => b.UsuPass).HasMaxLength(20).IsRequired();

            ////Productos
            //modelBuilder.Entity<Producto>().HasKey(b => b.ProID);
            //modelBuilder.Entity<Producto>().Property(b => b.ProID).ValueGeneratedOnAdd();
            //modelBuilder.Entity<Producto>().Property(b => b.ProDesc).HasMaxLength(50).IsRequired();
            //modelBuilder.Entity<Producto>().Property(b => b.ProValor).IsRequired();

            ////Pedidos
            //modelBuilder.Entity<Pedido>().HasKey(b => b.PedId);
            //modelBuilder.Entity<Pedido>().Property(b => b.PedId).ValueGeneratedOnAdd();
            //modelBuilder.Entity<Pedido>().Property(b => b.PedCant).IsRequired();
            //modelBuilder.Entity<Pedido>().Property(b => b.pedVrUnit).IsRequired();
            //modelBuilder.Entity<Pedido>().Property(b => b.PedIVA).IsRequired();
            //modelBuilder.Entity<Pedido>().Property(b => b.PedSubtot).IsRequired();
            //modelBuilder.Entity<Pedido>().Property(b => b.PedTotal).IsRequired();
            //modelBuilder.Entity<Pedido>().Property(b => b.ProductoProID).IsRequired();
            //modelBuilder.Entity<Pedido>().Property(b => b.UsuarioUsuID).IsRequired();

            modelBuilder.Entity<Pedido>().Property(t => t.PedSubtot).HasComputedColumnSql("CAST(pedVrUnit AS DECIMAL(18,2)) * CAST(PedCant AS DECIMAL(18,2))");
            modelBuilder.Entity<Pedido>().Property(t => t.PedTotal).HasComputedColumnSql("((CAST(pedVrUnit AS DECIMAL(18,2)) * CAST(PedCant AS DECIMAL(18,2))) * (PedIVA / 100)) + CAST(pedVrUnit AS DECIMAL(18,2)) * CAST(PedCant AS DECIMAL(18,2))");

            List<Usuario> usuarios = new List<Usuario>();
            usuarios.Add(new Usuario { UsuID = 1, UsuNombre = "Usuario1", UsuPass = Crypto.EncryptString("123").ToString() });
            usuarios.Add(new Usuario { UsuID = 2, UsuNombre = "Usuario2", UsuPass = Crypto.EncryptString("1234").ToString() });
            usuarios.Add(new Usuario { UsuID = 3, UsuNombre = "Usuario3", UsuPass = Crypto.EncryptString("12345").ToString() });

            modelBuilder.Entity<Usuario>().HasData(usuarios);

            List<Producto> productos = new List<Producto>();
            productos.Add(new Producto { ProID = 1, ProDesc = "Producto 1", ProValor = 12000 });
            productos.Add(new Producto { ProID = 2, ProDesc = "Producto 2", ProValor = 10000 });
            productos.Add(new Producto { ProID = 3, ProDesc = "Producto 3", ProValor = 8000 });

            modelBuilder.Entity<Producto>().HasData(productos);

            List<Pedido> pedidos = new List<Pedido>();
            pedidos.Add(new Pedido { PedId = 1, PedCant = 2, pedVrUnit = 12000, PedIVA = 19, ProductoProID = 1, UsuarioUsuID = 1 });
            pedidos.Add(new Pedido { PedId = 2, PedCant = 4, pedVrUnit = 8000, PedIVA = 19, ProductoProID = 2, UsuarioUsuID = 2 });
            pedidos.Add(new Pedido { PedId = 3, PedCant = 6, pedVrUnit = 6000, PedIVA = 19, ProductoProID = 3, UsuarioUsuID = 3 });

            modelBuilder.Entity<Pedido>().HasData(pedidos);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ApplicationUser> AppUsuario { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
    }
}