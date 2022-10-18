using System;
using System.Collections.Generic;

namespace TDS.Models
{
    public partial class Estudiante
    {
        public Estudiante()
        {
            Entregas = new HashSet<Entrega>();
            EstudiantesClases = new HashSet<EstudiantesClase>();
            Mensajes = new HashSet<Mensaje>();
            MensajesDetalles = new HashSet<MensajesDetalle>();
            Usuarios = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Correo { get; set; }
        public string? Codigo { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public bool? Estado { get; set; }
        public int? InstitucionId { get; set; }

        public virtual Institucion? Institucion { get; set; }
        public virtual ICollection<Entrega> Entregas { get; set; }
        public virtual ICollection<EstudiantesClase> EstudiantesClases { get; set; }
        public virtual ICollection<Mensaje> Mensajes { get; set; }
        public virtual ICollection<MensajesDetalle> MensajesDetalles { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
