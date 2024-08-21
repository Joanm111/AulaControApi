using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class ComentarioForo
{
    public int Id { get; set; }

    public int? ForoId { get; set; }

    public int? UsuarioId { get; set; }

    public string Comentario { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public virtual Foro? Foro { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
