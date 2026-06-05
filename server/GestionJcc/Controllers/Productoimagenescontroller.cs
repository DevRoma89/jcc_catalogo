using GestionJcc.DTOs;
using GestionJcc.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using GestionJcc.Datos;
namespace GestionJcc.Controllers
{
    [ApiController]
    [Route("api/productos/{productoId:int}/imagenes")]
    public class ProductoImagenesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductoImagenesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/productos/5/imageness
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoImagenDto>>> GetAll(int productoId)
        {
            var productoExiste = await _context.Productos.AnyAsync(p => p.Id == productoId);
            if (!productoExiste)
                return NotFound("Producto no encontrado.");

            var imagenes = await _context.ProductoImagenes
                .AsNoTracking()
                .Where(i => i.ProductoId == productoId)
                .ToListAsync();

            var resultado = imagenes.Select(i => new ProductoImagenDto
            {
                Id = i.Id,
                Url = i.Url,
                EsPrincipal = i.EsPrincipal
            });

            return Ok(resultado);
        }

        // GET api/productos/5/imagenes/2
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductoImagenDto>> GetById(int productoId, int id)
        {
            var imagen = await _context.ProductoImagenes
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id && i.ProductoId == productoId);

            if (imagen is null)
                return NotFound();

            var resultado = new ProductoImagenDto
            {
                Id = imagen.Id,
                Url = imagen.Url,
                EsPrincipal = imagen.EsPrincipal
            };

            return Ok(resultado);
        }

        // POST api/productos/5/imagenes
        [HttpPost]
        public async Task<ActionResult<ProductoImagenDto>> Create(int productoId, CrearProductoImagenDto dto)
        {
            var productoExiste = await _context.Productos.AnyAsync(p => p.Id == productoId);
            if (!productoExiste)
                return NotFound("Producto no encontrado.");

            if (dto.EsPrincipal)
            {
                var anteriores = await _context.ProductoImagenes
                    .Where(i => i.ProductoId == productoId && i.EsPrincipal)
                    .ToListAsync();

                foreach (var img in anteriores)
                    img.EsPrincipal = false;
            }

            var imagen = new ProductoImagen
            {
                ProductoId = productoId,
                Url = dto.Url,
                EsPrincipal = dto.EsPrincipal
            };

            _context.ProductoImagenes.Add(imagen);
            await _context.SaveChangesAsync();

            var resultado = new ProductoImagenDto
            {
                Id = imagen.Id,
                Url = imagen.Url,
                EsPrincipal = imagen.EsPrincipal
            };

            return CreatedAtAction(nameof(GetById), new { productoId, id = imagen.Id }, resultado);
        }

        // PUT api/productos/5/imagenes/2
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int productoId, int id, ActualizarProductoImagenDto dto)
        {
            var imagen = await _context.ProductoImagenes
                .FirstOrDefaultAsync(i => i.Id == id && i.ProductoId == productoId);

            if (imagen is null)
                return NotFound();

            if (dto.EsPrincipal && !imagen.EsPrincipal)
            {
                var anteriores = await _context.ProductoImagenes
                    .Where(i => i.ProductoId == productoId && i.EsPrincipal)
                    .ToListAsync();

                foreach (var img in anteriores)
                    img.EsPrincipal = false;
            }

            imagen.Url = dto.Url;
            imagen.EsPrincipal = dto.EsPrincipal;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/productos/5/imagenes/2
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int productoId, int id)
        {
            var imagen = await _context.ProductoImagenes
                .FirstOrDefaultAsync(i => i.Id == id && i.ProductoId == productoId);

            if (imagen is null)
                return NotFound();

            _context.ProductoImagenes.Remove(imagen);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}