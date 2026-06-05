namespace GestionJcc.DTOs
{
    // ─── Lectura ───────────────────────────────────────────────
    public class CategoriaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }

    public class CategoriaConProductosDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public List<ProductoDto> Productos { get; set; } = new();
    }

    // ─── Escritura ─────────────────────────────────────────────
    public class CrearCategoriaDto
    {
        public string Nombre { get; set; } = string.Empty;
    }

    public class ActualizarCategoriaDto
    {
        public string Nombre { get; set; } = string.Empty;
    }
}