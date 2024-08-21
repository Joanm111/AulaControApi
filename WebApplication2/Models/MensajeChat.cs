using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class MensajeChat
{
    public int Id { get; set; }

    public int? ChatId { get; set; }

    public int? RemitenteId { get; set; }

    public string Mensaje { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public bool Leido { get; set; }

    public virtual Chat? Chat { get; set; }

    public virtual Usuario? Remitente { get; set; }
}
