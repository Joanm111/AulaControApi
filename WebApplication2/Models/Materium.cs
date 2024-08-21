using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Materium
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int? ProfesorId { get; set; }

    public virtual ICollection<Calificacion> Calificacions { get; set; } = new List<Calificacion>();

    public virtual Profesor? Profesor { get; set; }
}
