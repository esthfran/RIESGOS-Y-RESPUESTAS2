using System;
using System.Collections.Generic;

namespace RIESGOS_Y_RESPUESTAS.Models;

public partial class Activo
{
    public int Idactivos { get; set; }

    public string? Implementos { get; set; }

    public string TiposDeActivosIdtipos { get; set; } = null!;

    public string CosteActivos { get; set; } = null!;

    public string Cantidad { get; set; } = null!;

    public int? Idmunicipalidad { get; set; }

    public virtual ICollection<Dañosformulario> Dañosformularios { get; set; } = new List<Dañosformulario>();

    public virtual MunicipalidadIqq? IdmunicipalidadNavigation { get; set; }
}
