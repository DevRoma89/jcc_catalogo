namespace GestionJcc.Entidades
{
    public class ProductoImagen
    {
        public int Id { get; set; }

        public int ProductoId { get; set; }

        // URL pública o ruta
        public string Url { get; set; } = string.Empty;

        public bool EsPrincipal { get; set; }

        public Producto Producto { get; set; } = null!;

    }
}
