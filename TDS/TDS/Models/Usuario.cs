using System;
using System.Collections.Generic;

namespace TDS.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? EstudianteId { get; set; }
        public int? MaestroId { get; set; }
        public int? RolId { get; set; }
        public int? InstitucionId { get; set; }
        public bool? Estado { get; set; }

        public virtual Estudiante? Estudiante { get; set; }
        public virtual Institucion? Institucion { get; set; }
        public virtual Maestro? Maestro { get; set; }
        public virtual Rol? Rol { get; set; }
    }
}
