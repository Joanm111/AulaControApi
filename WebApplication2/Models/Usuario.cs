using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Correo { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public int? RolId { get; set; }

    public virtual ICollection<ComentarioForo> ComentarioForos { get; set; } = new List<ComentarioForo>();

    public virtual ICollection<MensajeChat> MensajeChats { get; set; } = new List<MensajeChat>();

    public virtual ICollection<Notificacion> Notificacions { get; set; } = new List<Notificacion>();

    public virtual ICollection<Padre> Padres { get; set; } = new List<Padre>();

    public virtual ICollection<Profesor> Profesors { get; set; } = new List<Profesor>();

    public virtual Rol? Rol { get; set; }
}
