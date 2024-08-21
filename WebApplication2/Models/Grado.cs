using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Grado
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();

    public virtual ICollection<Foro> Foros { get; set; } = new List<Foro>();

    public virtual ICollection<Profesor> Profesors { get; set; } = new List<Profesor>();
}
