using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Estudiante
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public int? PadreId { get; set; }

    public int? GradoId { get; set; }

    public virtual ICollection<Asistencium> Asistencia { get; set; } = new List<Asistencium>();

    public virtual ICollection<Calificacion> Calificacions { get; set; } = new List<Calificacion>();

    public virtual ICollection<Conductum> Conducta { get; set; } = new List<Conductum>();

    public virtual Grado? Grado { get; set; }

    public virtual Padre? Padre { get; set; }

    public virtual ICollection<Reporte> Reportes { get; set; } = new List<Reporte>();
}
