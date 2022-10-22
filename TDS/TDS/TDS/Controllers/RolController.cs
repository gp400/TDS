using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TDS.Models;

namespace TDS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolController : Controller
    {
        private readonly TDSContext _context;

        public RolController(TDSContext context)
        {
            this._context = context;
        }

        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var roles = await _context.Rols.Where(x => x.Estado == true).AsNoTracking().ToListAsync();
                return Ok(roles);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("GetRol/{idRol}")]
        public async Task<IActionResult> GetRoles(int idRol)
        {
            try
            {
                var rol = await _context.Rols.Where(x => x.Id == idRol && x.Estado == true).AsNoTracking().FirstOrDefaultAsync();
                if (rol == null)
                {
                    return NotFound("No existe ese Rol");
                }
                return Ok(rol);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("InsertRol")]
        public async Task<IActionResult> InsertRol([FromBody] Rol rol)
        {
            try
            {
                rol.Estado = true;
                await _context.Rols.AddAsync(rol);
                await _context.SaveChangesAsync();
                return Ok(rol);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateRol")]
        public async Task<IActionResult> UpdateRol([FromBody] Rol rol)
        {
            try
            {
                var oldRol = await _context.Rols.FirstOrDefaultAsync(x => x.Id == rol.Id && x.Estado == true);
                if (oldRol == null)
                {
                    return BadRequest($"Asegurese de que sea un rol valido");
                }
                oldRol.Descripcion = rol.Descripcion;
                oldRol.Nombre = rol.Nombre;
                oldRol.Estado = true;
                await _context.SaveChangesAsync();
                return Ok(oldRol);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteRol/{idRol}")]
        public async Task<IActionResult> DeleteRol(int idRol)
        {
            try
            {
                var oldRol = await _context.Rols.FirstOrDefaultAsync(x => x.Id == idRol && x.Estado == true);
                if (oldRol == null)
                {
                    return BadRequest($"Asegurese de que sea un rol valido");
                }
                oldRol.Estado = false;
                await _context.SaveChangesAsync();
                return Ok(oldRol);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
