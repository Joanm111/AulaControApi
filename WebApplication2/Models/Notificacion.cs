using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Notificacion
{
    public int Id { get; set; }

    public int? UsuarioId { get; set; }

    public string Tipo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateOnly Fecha { get; set; }

    public bool Leido { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
