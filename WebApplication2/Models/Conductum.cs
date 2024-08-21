using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Conductum
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public int? EstudianteId { get; set; }

    public virtual Estudiante? Estudiante { get; set; }
}
