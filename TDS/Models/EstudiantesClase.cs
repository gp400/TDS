using System;
using System.Collections.Generic;

namespace TDS.Models
{
    public partial class EstudiantesClase
    {
        public int Id { get; set; }
        public int? EstudianteId { get; set; }
        public int? ClaseId { get; set; }

        public virtual Clase? Clase { get; set; }
        public virtual Estudiante? Estudiante { get; set; }
    }
}
