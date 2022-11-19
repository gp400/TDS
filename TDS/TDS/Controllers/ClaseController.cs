using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TDS.Models;

namespace TDS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClaseController : Controller
    {
        private readonly TDSContext _context;

        public ClaseController(TDSContext context)
        {
            this._context = context;
        }

        [HttpGet("GetClases/{idInstitucion}")]
        public async Task<IActionResult> GetClases(int idInstitucion)
        {
            try
            {
                var clases = await _context.Clases.Where(x => x.Estado == true && x.InstitucionId == idInstitucion).Include(x => x.EstudiantesClases.Where(e => e.Estudiante.Estado == true)).Include(x => x.Maestro).AsNoTracking().ToListAsync();
                return Ok(clases);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetClasesById/{idInstitucion}/{id}")]
        public async Task<IActionResult> GetClasesById(int idInstitucion, int id)
        {
            try
            {
                var clase = await _context.Clases.Where(x => x.Estado == true && x.InstitucionId == idInstitucion && x.Id == id).Include(x => x.EstudiantesClases.Where(e => e.Estudiante.Estado == true)).Include(x => x.Maestro).AsNoTracking().FirstOrDefaultAsync();
                if (clase == null)
                {
                    return NotFound("No existe esa clase");
                }
                return Ok(clase);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetClasesByMaestroId/{idInstitucion}/{idMaestro}")]
        public async Task<IActionResult> GetClasesByMaestroId(int idInstitucion, int idMaestro)
        {
            try
            {
                var clase = await _context.Clases.Where(x => x.Estado == true && x.InstitucionId == idInstitucion && x.MaestroId == idMaestro).Include(x => x.EstudiantesClases.Where(e => e.Estudiante.Estado == true)).Include(x => x.Maestro).AsNoTracking().ToListAsync();
                if (clase == null)
                {
                    return NotFound("No existe esa clase");
                }
                return Ok(clase);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetClasesByCodigo/{idInstitucion}/{codigo}")]
        public async Task<IActionResult> GetClasesByCodigo(int idInstitucion, string codigo)
        {
            try
            {
                var clase = await _context.Clases.Where(x => x.Estado == true && x.InstitucionId == idInstitucion && x.Codigo == codigo).Include(x => x.EstudiantesClases.Where(e => e.Estudiante.Estado == true)).Include(x => x.Maestro).AsNoTracking().FirstOrDefaultAsync();
                if (clase == null)
                {
                    return NotFound("No existe esa clase");
                }
                return Ok(clase);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("InsertClase")]
        public async Task<IActionResult> InsertClase([FromBody] Clase clase)
        {
            try
            {
                var existe = await _context.Clases.AsNoTracking().FirstOrDefaultAsync(x => x.Codigo == clase.Codigo && x.Estado == true && x.InstitucionId == clase.InstitucionId);
                if (existe != null)
                {
                    return BadRequest($"Ya existe una clase con codigo {clase.Codigo}");
                }
                clase.Estado = true;
                clase.Maestro = null;
                await _context.Clases.AddAsync(clase);
                await _context.SaveChangesAsync();
                return Ok(clase);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateClase")]
        public async Task<IActionResult> UpdateClase([FromBody] Clase clase)
        {
            try
            {
                if (await _context.Clases.AnyAsync(x => x.Id != clase.Id && x.Estado == true && x.InstitucionId == clase.InstitucionId && x.Codigo == clase.Codigo))
                {
                    return BadRequest($"Ya existe una clase con codigo {clase.Codigo}");
                }
                var oldClase = await _context.Clases.FirstOrDefaultAsync(x => x.Id == clase.Id && x.Estado == true && x.InstitucionId == clase.InstitucionId);
                if (oldClase == null)
                {
                    return BadRequest($"Asegurese de que sea una clase valida");
                }
                oldClase.Codigo = clase.Codigo;
                oldClase.Descripcion = clase.Descripcion;
                oldClase.MaestroId = clase.MaestroId;
                oldClase.Nombre = clase.Nombre;
                oldClase.HoraInicio = clase.HoraInicio;
                oldClase.HoraFin = clase.HoraFin;
                await _context.SaveChangesAsync();
                return Ok(oldClase);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteClase/{idInstitucion}/{id}")]
        public async Task<IActionResult> DeleteClase(int idInstitucion, int id)
        {
            try
            {
                var oldClase = await _context.Clases.FirstOrDefaultAsync(x => x.Id == id && x.Estado == true && x.InstitucionId == idInstitucion);
                if (oldClase == null)
                {
                    return BadRequest($"Asegurese de que sea una clase valida");
                }
                oldClase.Estado = false;
                await _context.SaveChangesAsync();
                return Ok(oldClase);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}