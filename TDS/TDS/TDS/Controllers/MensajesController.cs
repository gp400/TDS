using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TDS.Models;

namespace TDS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MensajesController : Controller
    {
        private readonly TDSContext _context;

        public MensajesController(TDSContext context)
        {
            this._context = context;
        }

        [HttpGet("GetMensajes/{idClase}")]
        public async Task<IActionResult> GetMensajes(int idClase)
        {
            try
            {
                var mensajes = await _context.Mensajes.Where(x => x.Estado == true && x.ClaseId == idClase).Include(x => x.MensajesDetalles).AsNoTracking().ToListAsync();
                return Ok(mensajes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetMensajeById/{idClase}/{id}")]
        public async Task<IActionResult> GetMensajeById(int idClase, int id)
        {
            try
            {
                var mensaje = await _context.Mensajes.Where(x => x.Estado == true && x.ClaseId == idClase && x.Id == id).Include(x => x.MensajesDetalles).AsNoTracking().FirstOrDefaultAsync();
                if (mensaje == null)
                {
                    return NotFound("No existe ese mensaje");
                }
                return Ok(mensaje);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("InsertMensaje")]
        public async Task<IActionResult> InsertMensaje([FromBody] Mensaje mensaje)
        {
            try
            {
                mensaje.Estado = true;
                await _context.Mensajes.AddAsync(mensaje);
                await _context.SaveChangesAsync();
                return Ok(mensaje);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateMensaje")]
        public async Task<IActionResult> UpdateMensaje([FromBody] Mensaje mensaje)
        {
            try
            {
                var oldMensajes = await _context.Mensajes.FirstOrDefaultAsync(x => x.Id == mensaje.Id && x.ClaseId == mensaje.ClaseId);
                if (oldMensajes == null)
                {
                    return BadRequest($"Asegurese de que sea un mensaje valido");
                }
                oldMensajes.ClaseId = mensaje.ClaseId;
                oldMensajes.EstudianteId = mensaje.EstudianteId;
                oldMensajes.Fecha = mensaje.Fecha;
                oldMensajes.Texto = mensaje.Texto;
                await _context.SaveChangesAsync();
                return Ok(oldMensajes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteMensaje/{idClase}/{id}")]
        public async Task<IActionResult> DeleteEntrega(int idClase, int id)
        {
            try
            {
                var oldMensaje = await _context.Mensajes.FirstOrDefaultAsync(x => x.Id == id && x.Estado == true && x.ClaseId == idClase);
                if (oldMensaje == null)
                {
                    return BadRequest($"Asegurese de que sea un mensaje valido");
                }
                oldMensaje.Estado = false;
                await _context.SaveChangesAsync();
                return Ok(oldMensaje);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("InsertMensajeDetalle")]
        public async Task<IActionResult> InsertMensajeDetalle([FromBody] MensajesDetalle mensajeDetalle)
        {
            try
            {
                await _context.MensajesDetalles.AddAsync(mensajeDetalle);
                await _context.SaveChangesAsync();
                return Ok(mensajeDetalle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateMensajeDetalle")]
        public async Task<IActionResult> UpdateMensajeDetalle([FromBody] MensajesDetalle mensajeDetalle)
        {
            try
            {
                var oldDetalle = await _context.MensajesDetalles.FirstOrDefaultAsync(x => x.Id == mensajeDetalle.Id && mensajeDetalle.ClaseId == x.ClaseId);
                if (oldDetalle == null)
                {
                    return BadRequest($"Asegurese de que sea un mensaje valido");
                }
                oldDetalle.ClaseId = mensajeDetalle.ClaseId;
                oldDetalle.EstudianteId = mensajeDetalle.EstudianteId;
                oldDetalle.Fecha = mensajeDetalle.Fecha;
                oldDetalle.Texto = mensajeDetalle.Texto;
                oldDetalle.MensajeId = mensajeDetalle.MensajeId;
                await _context.SaveChangesAsync();
                return Ok(oldDetalle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteMensajeDetalle/{idClase}/{id}")]
        public async Task<IActionResult> DeleteMensajeDetalle(int idClase, int id)
        {
            try
            {
                var oldDetalle = await _context.MensajesDetalles.FirstOrDefaultAsync(x => x.Id == id && x.ClaseId == idClase);
                if (oldDetalle == null)
                {
                    return BadRequest($"Asegurese de que sea un mensaje valido");
                }
                _context.Remove(oldDetalle);
                await _context.SaveChangesAsync();
                return Ok(oldDetalle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}