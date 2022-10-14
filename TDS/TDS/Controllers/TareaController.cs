using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TDS.Models;

namespace TDS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TareaController : Controller
    {
        private readonly TDSContext _context;

        public TareaController(TDSContext context)
        {
            this._context = context;
        }

        [HttpGet("GetTareas/{idClase}")]
        public async Task<IActionResult> GetMaestros(int idClase)
        {
            try
            {
                var tareas = await _context.Tareas.Where(x => x.Estado == true && x.IdClase == idClase).AsNoTracking().ToListAsync();
                return Ok(tareas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetTareaById/{idClase}/{id}")]
        public async Task<IActionResult> GetTareaById(int idClase, int id)
        {
            try
            {
                var tarea = await _context.Tareas.Where(x => x.Estado == true && x.IdClase == idClase && x.Id == id).AsNoTracking().FirstOrDefaultAsync();
                if (tarea == null)
                {
                    return NotFound("No existe esa tarea");
                }
                return Ok(tarea);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetTareaByCodigo/{idClase}/{codigo}")]
        public async Task<IActionResult> GetTareaByCodigo(int idClase, string codigo)
        {
            try
            {
                var tarea = await _context.Tareas.Where(x => x.Estado == true && x.IdClase == idClase && x.Codigo == codigo).AsNoTracking().FirstOrDefaultAsync();
                if (tarea == null)
                {
                    return NotFound("No existe esa tarea");
                }
                return Ok(tarea);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("InsertTarea")]
        public async Task<IActionResult> InsertTarea([FromBody] Tarea tarea)
        {
            try
            {
                var existe = await _context.Tareas.AsNoTracking().FirstOrDefaultAsync(x => x.Codigo == tarea.Codigo && x.Estado == true && x.IdClase == tarea.IdClase);
                if (existe != null)
                {
                    return BadRequest($"Ya existe un tarea con codigo {tarea.Codigo}");
                }
                tarea.Estado = true;
                await _context.Tareas.AddAsync(tarea);
                await _context.SaveChangesAsync();
                return Ok(tarea);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateTarea")]
        public async Task<IActionResult> UpdateTarea([FromBody] Tarea tarea)
        {
            try
            {
                var oldTarea = await _context.Tareas.FirstOrDefaultAsync(x => x.Id == tarea.Id && x.Estado == true && x.IdClase == tarea.IdClase);
                if (oldTarea == null)
                {
                    return BadRequest($"Asegurese de que sea una tarea valida");
                }
                oldTarea.Codigo = tarea.Codigo;
                oldTarea.Descripcion = tarea.Descripcion;
                oldTarea.FechaEntrega = tarea.FechaEntrega;
                oldTarea.IdClase = tarea.IdClase;
                oldTarea.Titulo = tarea.Titulo;
                await _context.SaveChangesAsync();
                return Ok(oldTarea);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteTarea/{idClase}/{id}")]
        public async Task<IActionResult> DeleteTarea(int idClase, int id)
        {
            try
            {
                var oldTarea = await _context.Tareas.FirstOrDefaultAsync(x => x.Id == id && x.Estado == true && x.IdClase == idClase);
                if (oldTarea == null)
                {
                    return BadRequest($"Asegurese de que sea una tarea valida");
                }
                oldTarea.Estado = false;
                await _context.SaveChangesAsync();
                return Ok(oldTarea);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
