using Microsoft.AspNetCore.Mvc;

namespace GestionJcc.Controllers
{
    [ApiController]
    [Route("api/imagenes")]
    public class ImagenesController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public ImagenesController(IWebHostEnvironment env)
        {
            _env = env;
        }

        // POST api/imagenes/upload
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
                return BadRequest("No se recibió ningún archivo.");

            var extensionesPermitidas = new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" };
            var extension = Path.GetExtension(archivo.FileName).ToLowerInvariant();

            if (!extensionesPermitidas.Contains(extension))
                return BadRequest("Formato de imagen no permitido.");

            // Guardar en wwwroot/imagenes
            var carpeta = Path.Combine(_env.WebRootPath, "imagenes");
            Directory.CreateDirectory(carpeta);

            var nombreArchivo = $"{Guid.NewGuid()}{extension}";
            var rutaCompleta = Path.Combine(carpeta, nombreArchivo);

            using (var stream = System.IO.File.Create(rutaCompleta))
            {
                await archivo.CopyToAsync(stream);
            }

            var url = $"{Request.Scheme}://{Request.Host}/imagenes/{nombreArchivo}";

            return Ok(new { url });
        }
    }
}