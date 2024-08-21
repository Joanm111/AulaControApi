using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Profesor
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public int? UsuarioId { get; set; }

    public int? GradoId { get; set; }

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public virtual ICollection<Foro> Foros { get; set; } = new List<Foro>();

    public virtual Grado? Grado { get; set; }

    public virtual ICollection<Materium> Materia { get; set; } = new List<Materium>();

    public virtual Usuario? Usuario { get; set; }
}
