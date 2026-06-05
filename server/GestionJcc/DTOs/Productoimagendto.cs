namespace GestionJcc.DTOs
{
    // ─── Lectura ───────────────────────────────────────────────
    public class ProductoImagenDto
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public bool EsPrincipal { get; set; }
    }

    // ─── Escritura ─────────────────────────────────────────────
    public class CrearProductoImagenDto
    {
        public string Url { get; set; } = string.Empty;
        public bool EsPrincipal { get; set; }
    }

    public class ActualizarProductoImagenDto
    {
        public string Url { get; set; } = string.Empty;
        public bool EsPrincipal { get; set; }
    }
}