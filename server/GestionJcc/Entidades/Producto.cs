namespace GestionJcc.Entidades
{
    public class Producto
    {
        public int Id { get; set; }

        public string Codigo { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public decimal PrecioVenta{get; set;}

        public bool Activo { get; set; } = true;

        public DateTime FechaCreacion { get; set; }
            = DateTime.UtcNow;

        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        // Relación
        public ICollection<ProductoImagen> Imagenes { get; set; }
            = new List<ProductoImagen>();
    }
}