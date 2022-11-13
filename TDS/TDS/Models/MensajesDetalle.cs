using System;
using System.Collections.Generic;

namespace TDS.Models
{
    public partial class MensajesDetalle
    {
        public int Id { get; set; }
        public string? Texto { get; set; }
        public int? ClaseId { get; set; }
        public int? EstudianteId { get; set; }
        public int? MensajeId { get; set; }
        public DateTime? Fecha { get; set; }
        public bool? Estado { get; set; }
        public int? MaestroId { get; set; }

        public virtual Clase? Clase { get; set; }
        public virtual Estudiante? Estudiante { get; set; }
        public virtual Maestro? Maestro { get; set; }
        public virtual Mensaje? Mensaje { get; set; }
    }
}
