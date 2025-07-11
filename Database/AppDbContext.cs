using Microsoft.EntityFrameworkCore;
using AduanasTec.Models;


namespace AduanasTec.Database
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venta> Ventas { get; set; }

        public DbSet<VentaProducto> VentaProductos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Tabla intermedia
            modelBuilder.Entity<VentaProducto>()
                .HasKey(vp => new { vp.VentaId, vp.ProductoId });

            modelBuilder.Entity<VentaProducto>()
                .HasOne(vp => vp.Venta)
                .WithMany(v => v.VentaProductos)
                .HasForeignKey(vp => vp.VentaId);

            modelBuilder.Entity<VentaProducto>()
                .HasOne(vp => vp.Producto)
                .WithMany(p => p.VentaProductos)
                .HasForeignKey(vp => vp.ProductoId);
        }





    }




}
