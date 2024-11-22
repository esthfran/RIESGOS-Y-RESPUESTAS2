using System;
using System.Collections.Generic;

namespace RIESGOS_Y_RESPUESTAS.Models;

public partial class Dañosformulario
{
    public int Iddañosimportantes { get; set; }

    public string? AreasAfectadas { get; set; }

    public string? ValorDaños { get; set; }

    public int? Respuesta { get; set; }

    public int? Activos { get; set; }

    public string? EstadoDaño { get; set; }

    public virtual Activo? ActivosNavigation { get; set; }

    public virtual ICollection<Respuestum> RespuestaIdrespuesta { get; set; } = new List<Respuestum>();

    public virtual ICollection<Riesgo> RiesgosIdreportes { get; set; } = new List<Riesgo>();
}
