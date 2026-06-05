using GestionJcc.Datos;
using GestionJcc.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionJcc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == dto.NombreUsuario
                                       && u.Password == dto.Password);

            if (usuario is null)
                return Unauthorized("Usuario o contraseña incorrectos.");

            return Ok(new { mensaje = "Login exitoso", usuario = usuario.NombreUsuario });
        }
    }
}