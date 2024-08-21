using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Foro
{
    public int Id { get; set; }

    public int? GradoId { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateOnly FechaCreacion { get; set; }

    public int? ProfesorId { get; set; }

    public virtual ICollection<ComentarioForo> ComentarioForos { get; set; } = new List<ComentarioForo>();

    public virtual Grado? Grado { get; set; }

    public virtual Profesor? Profesor { get; set; }
}
