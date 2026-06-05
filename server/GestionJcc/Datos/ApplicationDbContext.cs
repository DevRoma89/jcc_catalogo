using GestionJcc.Entidades;
using Microsoft.EntityFrameworkCore;

namespace GestionJcc.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }         
        public DbSet<ProductoImagen> ProductoImagenes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
