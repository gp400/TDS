using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TDS.Models;

namespace TDS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InstitucionController : Controller
    {
        private readonly TDSContext _context;

        public InstitucionController(TDSContext context)
        {
            this._context = context;
        }

        [HttpGet("GetInstituciones")]
        public async Task<IActionResult> GetInstituciones()
        {
            try
            {
                var instituciones = await _context.Institucions.Where(x => x.Estado == true).AsNoTracking().ToListAsync();
                return Ok(instituciones);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetInstitucionById/{id}")]
        public async Task<IActionResult> GetInstitucionById(int id)
        {
            try
            {
                var institucion = await _context.Institucions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Estado == true);
                if (institucion == null)
                {
                    return NotFound("No existe esa institucion");
                }
                return Ok(institucion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("GetInstitucionByCodigo/{codigo}")]
        public async Task<IActionResult> GetInstitucionById(string codigo)
        {
            try
            {
                var institucion = await _context.Institucions.AsNoTracking().FirstOrDefaultAsync(x => x.Codigo.ToLower() == codigo.ToLower() && x.Estado == true);
                if (institucion == null)
                {
                    return NotFound("No existe esa institucion");
                }
                return Ok(institucion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("InsertInstitucion")]
        public async Task<IActionResult> InsertInstitucion([FromBody] Institucion institucion)
        {
            try
            {
                var existe = await _context.Institucions.AsNoTracking().FirstOrDefaultAsync(x => x.Codigo == institucion.Codigo && x.Estado == true);
                if (existe != null)
                {
                    return BadRequest($"Ya existe una institucion con codigo {institucion.Codigo}");
                }
                institucion.Estado = true;
                await _context.Institucions.AddAsync(institucion);
                await _context.SaveChangesAsync();
                return Ok(institucion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut("UpdateInstitucion")]
        public async Task<IActionResult> UpdateInstitucion([FromBody] Institucion institucion)
        {
            try
            {
                if (await _context.Institucions.AnyAsync(x => x.Id != institucion.Id && x.Estado == true && x.Codigo == institucion.Codigo))
                {
                    return BadRequest($"Ya existe una institucion con codigo {institucion.Codigo}");
                }
                var oldInstitucion = await _context.Institucions.FirstOrDefaultAsync(x => x.Id == institucion.Id && x.Estado == true);
                if (oldInstitucion == null)
                {
                    return BadRequest($"Asegurese de que sea una institucion valida");
                }
                oldInstitucion.Nombre = (!string.IsNullOrEmpty(institucion.Nombre)) ? institucion.Nombre : oldInstitucion.Nombre;
                oldInstitucion.Descripcion = (!string.IsNullOrEmpty(institucion.Descripcion)) ? institucion.Descripcion : oldInstitucion.Descripcion;
                oldInstitucion.Codigo = (!string.IsNullOrEmpty(institucion.Codigo)) ? institucion.Codigo : oldInstitucion.Codigo;
                oldInstitucion.Direccion = (!string.IsNullOrEmpty(institucion.Direccion)) ? institucion.Direccion : oldInstitucion.Direccion;
                oldInstitucion.Correo = (!string.IsNullOrEmpty(institucion.Correo)) ? institucion.Correo : oldInstitucion.Correo;
                oldInstitucion.Telefono = (!string.IsNullOrEmpty(institucion.Telefono)) ? institucion.Telefono : oldInstitucion.Telefono;
                oldInstitucion.Licencias = (institucion.Licencias != null) ? institucion.Licencias : oldInstitucion.Licencias;
                await _context.SaveChangesAsync();
                return Ok(oldInstitucion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut("AgregarLicencias")]
        public async Task<IActionResult> AgregarLicencias([FromBody] AddLicencias addLicencias)
        {
            try
            {
                var oldInstitucion = await _context.Institucions.FirstOrDefaultAsync(x => x.Id == addLicencias.Id && x.Estado == true);
                if (oldInstitucion == null)
                {
                    return BadRequest($"Asegurese de que sea una institucion valida");
                }
                oldInstitucion.Licencias += addLicencias.Cantidad;
                await _context.SaveChangesAsync();
                return Ok(oldInstitucion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteInstitucion/{id}")]
        public async Task<IActionResult> DeleteInstitucion(int id)
        {
            try
            {
                var oldInstitucion = await _context.Institucions.FirstOrDefaultAsync(x => x.Id == id && x.Estado == true);
                if (oldInstitucion == null)
                {
                    return BadRequest($"Asegurese de que sea una institucion valida");
                }
                oldInstitucion.Estado = false;
                await _context.SaveChangesAsync();
                return Ok(oldInstitucion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class AddLicencias
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
    }
}
