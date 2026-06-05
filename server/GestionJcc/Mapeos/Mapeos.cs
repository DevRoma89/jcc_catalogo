using GestionJcc.DTOs;
using GestionJcc.Entidades;

namespace GestionJcc.Mapeos
{
    public static class CategoriaMapeo
    {
        public static CategoriaDto ToDto(this Categoria c) => new()
        {
            Id = c.Id,
            Nombre = c.Nombre
        };

        public static CategoriaConProductosDto ToDtoConProductos(this Categoria c) => new()
        {
            Id = c.Id,
            Nombre = c.Nombre,
            Productos = c.Productos.Select(p => p.ToDto()).ToList()
        };

        public static Categoria ToEntidad(this CrearCategoriaDto dto) => new()
        {
            Nombre = dto.Nombre
        };

        public static void ActualizarDesde(this Categoria c, ActualizarCategoriaDto dto)
        {
            c.Nombre = dto.Nombre;
        }
    }

    public static class ProductoMapeo
    {
        public static ProductoDto ToDto(this Producto p) => new()
        {
            Id = p.Id,
            Codigo = p.Codigo,
            Nombre = p.Nombre,
            Descripcion = p.Descripcion,
            PrecioVenta = p.PrecioVenta,
            Activo = p.Activo,
            FechaCreacion = p.FechaCreacion,
            Imagenes = p.Imagenes.Select(i => i.ToDto()).ToList()
        };

        public static Producto ToEntidad(this CrearProductoDto dto) => new()
        {
            Codigo = dto.Codigo,
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            PrecioVenta = dto.PrecioVenta,
            Imagenes = dto.Imagenes.Select(i => i.ToEntidad()).ToList()
        };

        public static void ActualizarDesde(this Producto p, ActualizarProductoDto dto)
        {
            p.Codigo = dto.Codigo;
            p.Nombre = dto.Nombre;
            p.Descripcion = dto.Descripcion;
            p.PrecioVenta = dto.PrecioVenta;
            p.Activo = dto.Activo;
        }
    }

    public static class ProductoImagenMapeo
    {
        public static ProductoImagenDto ToDto(this ProductoImagen i) => new()
        {
            Id = i.Id,
            Url = i.Url,
            EsPrincipal = i.EsPrincipal
        };

        public static ProductoImagen ToEntidad(this CrearProductoImagenDto dto) => new()
        {
            Url = dto.Url,
            EsPrincipal = dto.EsPrincipal
        };

        public static void ActualizarDesde(this ProductoImagen img, ActualizarProductoImagenDto dto)
        {
            img.Url = dto.Url;
            img.EsPrincipal = dto.EsPrincipal;
        }
    }
}