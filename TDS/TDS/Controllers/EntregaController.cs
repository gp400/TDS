using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TDS.Models;

namespace TDS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EntregaController : Controller
    {
        private readonly TDSContext _context;

        public EntregaController(TDSContext context)
        {
            this._context = context;
        }

        [HttpGet("GetEntregas/{TareaId}")]
        public async Task<IActionResult> GetEntregas(int TareaId)
        {
            try
            {
                var entregas = await _context.Entregas.Where(x => x.Estado == true && x.TareaId == TareaId).Include(t => t.Estudiante).Include(t => t.Tarea).AsNoTracking().ToListAsync();
                return Ok(entregas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetEntregaById/{TareaId}/{id}")]
        public async Task<IActionResult> GetEntregaById(int TareaId, int id)
        {
            try
            {
                var entrega = await _context.Entregas.Where(x => x.Estado == true && x.TareaId == TareaId && x.Id == id).Include(t => t.Estudiante).Include(t => t.Tarea).AsNoTracking().FirstOrDefaultAsync();
                if (entrega == null)
                {
                    return NotFound("No existe esa entrega");
                }
                return Ok(entrega);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("InsertEntrega")]
        public async Task<IActionResult> InsertEntrega([FromBody] Entrega entrega)
        {
            try
            {
                var existe = await _context.Entregas.AsNoTracking().FirstOrDefaultAsync(x => x.TareaId == entrega.TareaId && x.Estado == true && x.EstudianteId == entrega.EstudianteId);
                if (existe != null)
                {
                    return BadRequest($"El estudiante ya entrego esta tarea");
                }
                entrega.Estudiante = null;
                entrega.Tarea = null;
                entrega.Estado = true;
                entrega.Estudiante = null;
                entrega.Tarea = null;
                await _context.Entregas.AddAsync(entrega);
                await _context.SaveChangesAsync();
                return Ok(entrega);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateEntrega")]
        public async Task<IActionResult> UpdateEntrega([FromBody] Entrega entrega)
        {
            try
            {
                var oldEntrega = await _context.Entregas.FirstOrDefaultAsync(x => x.Id == entrega.Id && x.Estado == true && x.TareaId == entrega.TareaId && entrega.EstudianteId == x.EstudianteId);
                if (oldEntrega == null)
                {
                    return BadRequest($"Asegurese de que sea una entrega valida");
                }
                entrega.Estudiante = null;
                entrega.Tarea = null;
                oldEntrega.Documento = entrega.Documento;
                oldEntrega.EstudianteId = entrega.EstudianteId;
                oldEntrega.TareaId = entrega.TareaId;
                oldEntrega.Calificacion = entrega.Calificacion;
                await _context.SaveChangesAsync();
                return Ok(oldEntrega);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteEntrega/{idTarea}/{id}")]
        public async Task<IActionResult> DeleteEntrega(int idTarea, int id)
        {
            try
            {
                var oldEntrega = await _context.Entregas.FirstOrDefaultAsync(x => x.Id == id && x.Estado == true && x.TareaId == idTarea);
                if (oldEntrega == null)
                {
                    return BadRequest($"Asegurese de que sea una entrega valida");
                }
                oldEntrega.Estado = false;
                await _context.SaveChangesAsync();
                return Ok(oldEntrega);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
