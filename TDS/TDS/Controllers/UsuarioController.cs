using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using TDS.Models;

namespace TDS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        private readonly TDSContext _context;

        public UsuarioController(TDSContext context)
        {
            this._context = context;
        }

        [HttpGet("GetUsuarios/{idInstitucion}")]
        public async Task<IActionResult> GetUsuarios(int idInstitucion)
        {
            try
            {
                var usuarios = await _context.Usuarios.Where(x => x.Estado == true && x.InstitucionId == idInstitucion).Include(x => x.Estudiante).Include(x => x.Maestro).AsNoTracking().ToListAsync();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUsuarioById/{idInstitucion}/{id}")]
        public async Task<IActionResult> GetUsuarioById(int idInstitucion, int id)
        {
            try
            {
                var usuario = await _context.Usuarios.Where(x => x.Estado == true && x.InstitucionId == idInstitucion && x.Id == id).Include(x => x.Estudiante).Include(x => x.Maestro).AsNoTracking().FirstOrDefaultAsync();
                if (usuario == null)
                {
                    return NotFound("No existe ese usuario");
                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("InsertUsuario")]
        public async Task<IActionResult> InsertUsuario([FromBody] Usuario usuario)
        {
            try
            {
                usuario.Estado = true;
                usuario.Password = this.HashPassword(usuario.Password);
                await _context.Usuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateUsuario")]
        public async Task<IActionResult> UpdateUsuario([FromBody] Usuario usuario)
        {
            try
            {
                var oldUsuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == usuario.Id && x.Estado == true && x.InstitucionId == usuario.InstitucionId);
                if (oldUsuario == null)
                {
                    return BadRequest($"Asegurese de que sea un usuario valido");
                }
                oldUsuario.Estado = true;
                oldUsuario.Password = this.HashPassword(usuario.Password);
                oldUsuario.EstudianteId = usuario.EstudianteId;
                oldUsuario.MaestroId = usuario.MaestroId;
                oldUsuario.RolId = usuario.RolId;
                await _context.SaveChangesAsync();
                return Ok(oldUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteUsuario/{idInstitucion}/{id}")]
        public async Task<IActionResult> DeleteUsuario(int idInstitucion, int id)
        {
            try
            {
                var oldUsuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id && x.Estado == true && x.InstitucionId == idInstitucion);
                if (oldUsuario == null)
                {
                    return BadRequest($"Asegurese de que sea un usuario valido");
                }
                oldUsuario.Estado = false;
                await _context.SaveChangesAsync();
                return Ok(oldUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Login/{correo}/{password}")]
        public async Task<IActionResult> Login(string correo, string password)
        {
            try
            {
                var usuario = await _context.Usuarios.Include(x => x.Maestro).Include(x => x.Institucion).Where(x => x.Maestro != null).FirstOrDefaultAsync(x => x.Maestro.Correo == correo && this.HashPassword(password) == x.Password);
                if (usuario == null)
                {
                    usuario = await _context.Usuarios.Include(x => x.Estudiante).Include(x => x.Institucion).Where(x => x.Estudiante != null).FirstOrDefaultAsync(x => x.Estudiante.Correo == correo && this.HashPassword(password) == x.Password);
                    if (usuario != null)
                    {
                        return Ok(usuario);
                    } else
                    {
                        return BadRequest("No existe ese usuario");
                    }
                } else
                {
                    return Ok(usuario);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [NonAction]
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            var sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
