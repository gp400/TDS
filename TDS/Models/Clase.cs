using System;
using System.Collections.Generic;

namespace TDS.Models
{
    public partial class Clase
    {
        public Clase()
        {
            EstudiantesClases = new HashSet<EstudiantesClase>();
            Mensajes = new HashSet<Mensaje>();
            MensajesDetalles = new HashSet<MensajesDetalle>();
            Tareas = new HashSet<Tarea>();
        }

        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int? MaestroId { get; set; }
        public int? InstitucionId { get; set; }
        public string? Codigo { get; set; }
        public bool? Estado { get; set; }

        public virtual Institucion? Institucion { get; set; }
        public virtual Maestro? Maestro { get; set; }
        public virtual ICollection<EstudiantesClase> EstudiantesClases { get; set; }
        public virtual ICollection<Mensaje> Mensajes { get; set; }
        public virtual ICollection<MensajesDetalle> MensajesDetalles { get; set; }
        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}
