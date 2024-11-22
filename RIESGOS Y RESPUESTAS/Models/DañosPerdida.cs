using System;
using System.Collections.Generic;

namespace RIESGOS_Y_RESPUESTAS.Models;

public partial class DañosPerdida
{
    public int Iddaños { get; set; }

    public string AreasAfectadas { get; set; } = null!;

    public decimal ValorDaños { get; set; }

    public int Respuesta { get; set; }

    public int Idactivos { get; set; }

    public string EstadoDaño { get; set; } = null!;

    public virtual Activo IdactivosNavigation { get; set; } = null!;

    public virtual ICollection<Respuestum> RespuestaIdrespuesta { get; set; } = new List<Respuestum>();

    public virtual ICollection<Riesgo> RiesgosIdreportes { get; set; } = new List<Riesgo>();
}
