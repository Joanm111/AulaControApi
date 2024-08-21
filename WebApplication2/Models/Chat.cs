using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Chat
{
    public int Id { get; set; }

    public int? ProfesorId { get; set; }

    public int? PadreId { get; set; }

    public virtual ICollection<MensajeChat> MensajeChats { get; set; } = new List<MensajeChat>();

    public virtual Padre? Padre { get; set; }

    public virtual Profesor? Profesor { get; set; }
}
