using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TDS.Models;

namespace TDS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaestroController : Controller
    {
        private readonly TDSContext _context;

        public MaestroController(TDSContext context)
        {
            this._context = context;
        }

        [HttpGet("GetMaestros/{idInstitucion}")]
        public async Task<IActionResult> GetMaestros(int idInstitucion)
        {
            try
            {
                var clases = await _context.Maestros.Where(x => x.Estado == true && x.InstitucionId == idInstitucion).AsNoTracking().ToListAsync();
                return Ok(clases);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetMaestroById/{idInstitucion}/{id}")]
        public async Task<IActionResult> GetMaestroById(int idInstitucion, int id)
        {
            try
            {
                var clase = await _context.Maestros.Where(x => x.Estado == true && x.InstitucionId == idInstitucion && x.Id == id).AsNoTracking().FirstOrDefaultAsync();
                if (clase == null)
                {
                    return NotFound("No existe ese maestro");
                }
                return Ok(clase);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetMaestroByCodigo/{idInstitucion}/{codigo}")]
        public async Task<IActionResult> GetMaestroByCodigo(int idInstitucion, string codigo)
        {
            try
            {
                var clase = await _context.Maestros.Where(x => x.Estado == true && x.InstitucionId == idInstitucion && x.Codigo == codigo).AsNoTracking().FirstOrDefaultAsync();
                if (clase == null)
                {
                    return NotFound("No existe ese maestro");
                }
                return Ok(clase);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("InsertMaestro")]
        public async Task<IActionResult> InsertMaestro([FromBody] Maestro maestro)
        {
            try
            {
                var existe = await _context.Maestros.AsNoTracking().FirstOrDefaultAsync(x => x.Codigo == maestro.Codigo && x.Estado == true && x.InstitucionId == maestro.InstitucionId);
                if (existe != null)
                {
                    return BadRequest($"Ya existe un maestro con codigo {maestro.Codigo}");
                }
                maestro.Estado = true;
                await _context.Maestros.AddAsync(maestro);
                await _context.SaveChangesAsync();
                return Ok(maestro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateMaestro")]
        public async Task<IActionResult> UpdateMaestro([FromBody] Maestro maestro)
        {
            try
            {
                var oldMaestro = await _context.Maestros.FirstOrDefaultAsync(x => x.Id == maestro.Id && x.Estado == true && x.InstitucionId == maestro.InstitucionId);
                if (oldMaestro == null)
                {
                    return BadRequest($"Asegurese de que sea un maestro valido");
                }
                oldMaestro.Apellidos = maestro.Apellidos;
                oldMaestro.Codigo = maestro.Codigo;
                oldMaestro.Correo = maestro.Correo;
                oldMaestro.Direccion = maestro.Direccion;
                oldMaestro.Nombres = maestro.Nombres;
                oldMaestro.Telefono = maestro.Telefono;
                await _context.SaveChangesAsync();
                return Ok(oldMaestro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteMaestro/{idInstitucion}/{id}")]
        public async Task<IActionResult> DeleteMaestro(int idInstitucion, int id)
        {
            try
            {
                var oldMaestro = await _context.Maestros.FirstOrDefaultAsync(x => x.Id == id && x.Estado == true && x.InstitucionId == idInstitucion);
                if (oldMaestro == null)
                {
                    return BadRequest($"Asegurese de que sea un maestro valido");
                }
                oldMaestro.Estado = false;
                await _context.SaveChangesAsync();
                return Ok(oldMaestro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
