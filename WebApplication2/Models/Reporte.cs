using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Reporte
{
    public int Id { get; set; }

    public int? EstudianteId { get; set; }

    public DateOnly Fecha { get; set; }

    public string Tipo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual Estudiante? Estudiante { get; set; }
}
