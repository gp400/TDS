using System;
using System.Collections.Generic;

namespace TDS.Models
{
    public partial class Entrega
    {
        public int Id { get; set; }
        public string? Documento { get; set; }
        public int? TareaId { get; set; }
        public int? EstudianteId { get; set; }
        public bool? Estado { get; set; }

        public virtual Estudiante? Estudiante { get; set; }
        public virtual Tarea? Tarea { get; set; }
    }
}
