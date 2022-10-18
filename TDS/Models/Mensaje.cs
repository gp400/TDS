using System;
using System.Collections.Generic;

namespace TDS.Models
{
    public partial class Mensaje
    {
        public Mensaje()
        {
            MensajesDetalles = new HashSet<MensajesDetalle>();
        }

        public int Id { get; set; }
        public string? Texto { get; set; }
        public int? ClaseId { get; set; }
        public int? EstudianteId { get; set; }
        public bool? Estado { get; set; }
        public DateTime? Fecha { get; set; }

        public virtual Clase? Clase { get; set; }
        public virtual Estudiante? Estudiante { get; set; }
        public virtual ICollection<MensajesDetalle> MensajesDetalles { get; set; }
    }
}
