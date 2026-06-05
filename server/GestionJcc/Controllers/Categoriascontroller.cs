using GestionJcc.DTOs;
using GestionJcc.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using GestionJcc.Datos;

namespace GestionJcc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;


        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> GetAll()
        {
            var categorias = await _context.Categorias
                .AsNoTracking()
                .ToListAsync();

            var resultado = categorias.Select(c => new CategoriaDto
            {
                Id = c.Id,
                Nombre = c.Nombre
            });

            return Ok(resultado);
        }

        // GET api/categorias/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoriaConProductosDto>> GetById(int id)
        {
            var categoria = await _context.Categorias
                .AsNoTracking()
                .Include(c => c.Productos)
                    .ThenInclude(p => p.Imagenes)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (categoria is null)
                return NotFound();

            var resultado = new CategoriaConProductosDto
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
                Productos = categoria.Productos.Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Codigo = p.Codigo,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    PrecioVenta = p.PrecioVenta,
                    Activo = p.Activo,
                    FechaCreacion = p.FechaCreacion,
                    Imagenes = p.Imagenes.Select(i => new ProductoImagenDto
                    {
                        Id = i.Id,
                        Url = i.Url,
                        EsPrincipal = i.EsPrincipal
                    }).ToList()
                }).ToList()
            };

            return Ok(resultado);
        }

        // POST api/categorias
        [HttpPost]
        public async Task<ActionResult<CategoriaDto>> Create(CrearCategoriaDto dto)
        {
            var categoria = new Categoria
            {
                Nombre = dto.Nombre
            };

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            var resultado = new CategoriaDto
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre
            };

            return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, resultado);
        }

        // PUT api/categorias/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, ActualizarCategoriaDto dto)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria is null)
                return NotFound();

            categoria.Nombre = dto.Nombre;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/categorias/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria is null)
                return NotFound();

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}