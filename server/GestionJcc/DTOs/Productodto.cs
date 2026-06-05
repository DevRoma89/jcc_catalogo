namespace GestionJcc.DTOs
{
    // ─── Lectura ───────────────────────────────────────────────
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal PrecioVenta { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int CategoriaId { get; set; }
        public List<ProductoImagenDto> Imagenes { get; set; } = new();
    }

    // ─── Escritura ─────────────────────────────────────────────
    public class CrearProductoDto
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal PrecioVenta { get; set; }
        public int CategoriaId { get; set; }
        public List<CrearProductoImagenDto> Imagenes { get; set; } = new();
    }

    public class ActualizarProductoDto
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal PrecioVenta { get; set; }
        public bool Activo { get; set; }
        public int CategoriaId { get; set; }
    }
}