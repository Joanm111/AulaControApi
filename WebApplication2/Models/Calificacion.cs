using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Calificacion
{
    public int Id { get; set; }

    public int? EstudianteId { get; set; }

    public int? MateriaId { get; set; }

    public int Valor1 { get; set; }

    public int Valor2 { get; set; }

    public int Valor3 { get; set; }

    public virtual Estudiante? Estudiante { get; set; }

    public virtual Materium? Materia { get; set; }
}
