using System;
using System.Collections.Generic;

namespace TDS.Models
{
    public partial class Institucion
    {
        public Institucion()
        {
            Clases = new HashSet<Clase>();
            Estudiantes = new HashSet<Estudiante>();
            Maestros = new HashSet<Maestro>();
        }

        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Codigo { get; set; }
        public string? Direccion { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public int? Licencias { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<Clase> Clases { get; set; }
        public virtual ICollection<Estudiante> Estudiantes { get; set; }
        public virtual ICollection<Maestro> Maestros { get; set; }
    }
}
