using System;
using System.Collections.Generic;

namespace TDS.Models
{
    public partial class Tarea
    {
        public Tarea()
        {
            Entregas = new HashSet<Entrega>();
        }

        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public int? IdClase { get; set; }
        public bool? Estado { get; set; }
        public string? Codigo { get; set; }

        public virtual Clase? IdClaseNavigation { get; set; }
        public virtual ICollection<Entrega> Entregas { get; set; }
    }
}
