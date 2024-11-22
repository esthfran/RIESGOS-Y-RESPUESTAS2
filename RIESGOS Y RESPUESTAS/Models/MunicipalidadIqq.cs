using System;
using System.Collections.Generic;

namespace RIESGOS_Y_RESPUESTAS.Models;

public partial class MunicipalidadIqq
{
    public int Idmunicipalidad { get; set; }

    public string NombreDirección { get; set; } = null!;

    public string NombreDepartamento { get; set; } = null!;

    public virtual ICollection<Activo> Activos { get; set; } = new List<Activo>();

    public virtual ICollection<Riesgo> Riesgos { get; set; } = new List<Riesgo>();
}
