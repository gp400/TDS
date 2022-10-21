using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                var usuarios = await _context.Usuarios.Where(x => x.Estado == true && x.InstitucionId == idInstitucion).AsNoTracking().ToListAsync();
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
                var usuario = await _context.Usuarios.Where(x => x.Estado == true && x.InstitucionId == idInstitucion && x.Id == id).AsNoTracking().FirstOrDefaultAsync();
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
                oldUsuario.Email = usuario.Email;
                oldUsuario.Password = usuario.Password;
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
    }
}
