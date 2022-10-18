using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TDS.Models;

namespace TDS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstudiantesController : Controller
    {
        private readonly TDSContext _context;

        public EstudiantesController(TDSContext context)
        {
            this._context = context;
        }

        [HttpGet("GetEstudiantes/{idInstitucion}")]
        public async Task<IActionResult> GetEstudiantes(int idInstitucion)
        {
            try
            {
                var clases = await _context.Estudiantes.Where(x => x.Estado == true && x.InstitucionId == idInstitucion).AsNoTracking().ToListAsync();
                return Ok(clases);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetEstudianteById/{idInstitucion}/{id}")]
        public async Task<IActionResult> GetEstudianteById(int idInstitucion, int id)
        {
            try
            {
                var clase = await _context.Estudiantes.Where(x => x.Estado == true && x.InstitucionId == idInstitucion && x.Id == id).AsNoTracking().FirstOrDefaultAsync();
                if (clase == null)
                {
                    return NotFound("No existe ese estudiante");
                }
                return Ok(clase);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetEstudianteByCodigo/{idInstitucion}/{codigo}")]
        public async Task<IActionResult> GetEstudianteByCodigo(int idInstitucion, string codigo)
        {
            try
            {
                var clase = await _context.Estudiantes.Where(x => x.Estado == true && x.InstitucionId == idInstitucion && x.Codigo == codigo).AsNoTracking().FirstOrDefaultAsync();
                if (clase == null)
                {
                    return NotFound("No existe ese estudiante");
                }
                return Ok(clase);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("InsertEstudiante")]
        public async Task<IActionResult> InsertEstudiante([FromBody] Estudiante estudiante)
        {
            try
            {
                var existe = await _context.Estudiantes.AsNoTracking().FirstOrDefaultAsync(x => x.Codigo == estudiante.Codigo && x.Estado == true && x.InstitucionId == estudiante.InstitucionId);
                if (existe != null)
                {
                    return BadRequest($"Ya existe un estudiante con codigo {estudiante.Codigo}");
                }
                estudiante.Estado = true;
                await _context.Estudiantes.AddAsync(estudiante);
                await _context.SaveChangesAsync();
                return Ok(estudiante);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetEstudianteClase/{ClaseId}/{EstudianteId}")]
        public async Task<IActionResult> GetEstudianteClase(int ClaseId, int EstudianteId)
        {
            try
            {
                var listado = await _context.EstudiantesClases.Where(x => x.ClaseId == ClaseId && x.EstudianteId == EstudianteId).ToListAsync();
                return Ok(listado);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AgregarAClase")]
        public async Task<IActionResult> AgregarAClase([FromBody] EstudiantesClase estudiantesClase)
        {
            try
            {
                await _context.EstudiantesClases.AddAsync(estudiantesClase);
                await _context.SaveChangesAsync();
                return Ok(estudiantesClase);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("EliminarDeClase/{id}")]
        public async Task<IActionResult> EliminarDeClase(int id)
        {
            try
            {
                var ec = await _context.EstudiantesClases.FirstOrDefaultAsync(x => x.Id == id);
                if (ec == null)
                {
                    return NotFound($"El estudiante no se encuentra en esa clase");
                }
                _context.EstudiantesClases.Remove(ec);
                await _context.SaveChangesAsync();
                return Ok(ec);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateEstudiante")]
        public async Task<IActionResult> UpdateEstudiante([FromBody] Estudiante estudiante)
        {
            try
            {
                var oldEstudiante = await _context.Estudiantes.FirstOrDefaultAsync(x => x.Id == estudiante.Id && x.Estado == true && x.InstitucionId == estudiante.InstitucionId);
                if (oldEstudiante == null)
                {
                    return BadRequest($"Asegurese de que sea un estudiante valido");
                }
                oldEstudiante.Apellidos = estudiante.Apellidos;
                oldEstudiante.Codigo = estudiante.Codigo;
                oldEstudiante.Correo = estudiante.Correo;
                oldEstudiante.Direccion = estudiante.Direccion;
                oldEstudiante.Nombres = estudiante.Nombres;
                oldEstudiante.Telefono = estudiante.Telefono;
                await _context.SaveChangesAsync();
                return Ok(oldEstudiante);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteEstudiante/{idInstitucion}/{id}")]
        public async Task<IActionResult> DeleteEstudiante(int idInstitucion, int id)
        {
            try
            {
                var oldEstudiante = await _context.Estudiantes.FirstOrDefaultAsync(x => x.Id == id && x.Estado == true && x.InstitucionId == idInstitucion);
                if (oldEstudiante == null)
                {
                    return BadRequest($"Asegurese de que sea un maestro valido");
                }
                oldEstudiante.Estado = false;
                await _context.SaveChangesAsync();
                return Ok(oldEstudiante);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
