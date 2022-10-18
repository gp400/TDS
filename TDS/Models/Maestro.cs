using System;
using System.Collections.Generic;

namespace TDS.Models
{
    public partial class Maestro
    {
        public Maestro()
        {
            Clases = new HashSet<Clase>();
            Usuarios = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public int? InstitucionId { get; set; }
        public bool? Estado { get; set; }
        public string? Codigo { get; set; }

        public virtual Institucion? Institucion { get; set; }
        public virtual ICollection<Clase> Clases { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
