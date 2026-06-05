using GestionJcc.DTOs;
using GestionJcc.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionJcc.Datos;

namespace GestionJcc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> GetAll()
        {
            var productos = await _context.Productos
                .AsNoTracking()
                .Include(p => p.Imagenes)
                .ToListAsync();

            var resultado = productos.Select(p => new ProductoDto
            {
                Id = p.Id,
                Codigo = p.Codigo,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                PrecioVenta = p.PrecioVenta,
                Activo = p.Activo,
                FechaCreacion = p.FechaCreacion,
                CategoriaId = p.CategoriaId,   
                Imagenes = p.Imagenes.Select(i => new ProductoImagenDto
                {
                    Id = i.Id,
                    Url = i.Url,
                    EsPrincipal = i.EsPrincipal
                }).ToList()
            });

            return Ok(resultado);
        }

        // GET api/productos/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductoDto>> GetById(int id)
        {
            var producto = await _context.Productos
                .AsNoTracking()
                .Include(p => p.Imagenes)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto is null)
                return NotFound();

            var resultado = new ProductoDto
            {
                Id = producto.Id,
                Codigo = producto.Codigo,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                PrecioVenta = producto.PrecioVenta,
                Activo = producto.Activo,
                FechaCreacion = producto.FechaCreacion,
                CategoriaId = producto.CategoriaId,   
                Imagenes = producto.Imagenes.Select(i => new ProductoImagenDto
                {
                    Id = i.Id,
                    Url = i.Url,
                    EsPrincipal = i.EsPrincipal
                }).ToList()
            };

            return Ok(resultado);
        }

        // GET api/productos/porCategoria/3
        [HttpGet("porCategoria/{categoriaId:int}")]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> GetByCategoria(int categoriaId)
        {
            var categoriaExiste = await _context.Categorias.AnyAsync(c => c.Id == categoriaId);
            if (!categoriaExiste)
                return NotFound("Categoría no encontrada.");

            var productos = await _context.Productos
                .AsNoTracking()
                .Where(p => p.CategoriaId == categoriaId)
                .Include(p => p.Imagenes)
                .ToListAsync();

            var resultado = productos.Select(p => new ProductoDto
            {
                Id = p.Id,
                Codigo = p.Codigo,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                PrecioVenta = p.PrecioVenta,
                Activo = p.Activo,
                FechaCreacion = p.FechaCreacion,
                CategoriaId = p.CategoriaId,   
                Imagenes = p.Imagenes.Select(i => new ProductoImagenDto
                {
                    Id = i.Id,
                    Url = i.Url,
                    EsPrincipal = i.EsPrincipal
                }).ToList()
            });

            return Ok(resultado);
        }

        // POST api/productos
        [HttpPost]
        public async Task<ActionResult<ProductoDto>> Create(CrearProductoDto dto)
        {
            var categoriaExiste = await _context.Categorias.AnyAsync(c => c.Id == dto.CategoriaId);
            if (!categoriaExiste)
                return BadRequest("La categoría especificada no existe.");

            var producto = new Producto
            {
                Codigo = dto.Codigo,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                PrecioVenta = dto.PrecioVenta,
                CategoriaId = dto.CategoriaId,
                Imagenes = dto.Imagenes.Select(i => new ProductoImagen
                {
                    Url = i.Url,
                    EsPrincipal = i.EsPrincipal
                }).ToList()
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            var resultado = new ProductoDto
            {
                Id = producto.Id,
                Codigo = producto.Codigo,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                PrecioVenta = producto.PrecioVenta,
                Activo = producto.Activo,
                FechaCreacion = producto.FechaCreacion,
                CategoriaId = producto.CategoriaId,   // ← agregado
                Imagenes = producto.Imagenes.Select(i => new ProductoImagenDto
                {
                    Id = i.Id,
                    Url = i.Url,
                    EsPrincipal = i.EsPrincipal
                }).ToList()
            };

            return CreatedAtAction(nameof(GetById), new { id = producto.Id }, resultado);
        }

        // PUT api/productos/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, ActualizarProductoDto dto)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto is null)
                return NotFound();

            var categoriaExiste = await _context.Categorias.AnyAsync(c => c.Id == dto.CategoriaId);
            if (!categoriaExiste)
                return BadRequest("La categoría especificada no existe.");

            producto.Codigo = dto.Codigo;
            producto.Nombre = dto.Nombre;
            producto.Descripcion = dto.Descripcion;
            producto.PrecioVenta = dto.PrecioVenta;
            producto.Activo = dto.Activo;
            producto.CategoriaId = dto.CategoriaId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/productos/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto is null)
                return NotFound();

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}