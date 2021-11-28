using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FinalLaboratorio4.Models;

namespace FinalLaboratorio4.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Producto>()
                .Property(p => p.Favorito)
                .HasDefaultValue(false);
            modelBuilder.Entity<Producto>().Property(p => p.Nombre).UseCollation("NOCASE");

            modelBuilder.Entity<Categoria>().Property(c => c.Descripcion).UseCollation("NOCASE");
            modelBuilder.Entity<Categoria>().HasIndex(c => c.Descripcion).IsUnique();

            modelBuilder.Entity<Marca>().Property(m => m.Descripcion).UseCollation("NOCASE");

            modelBuilder.Entity<Proveedor>().Property(p => p.Nombre).UseCollation("NOCASE");
        }


        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
    }
}
