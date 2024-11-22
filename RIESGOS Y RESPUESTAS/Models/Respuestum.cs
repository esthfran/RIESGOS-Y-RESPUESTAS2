using System;
using System.Collections.Generic;

namespace RIESGOS_Y_RESPUESTAS.Models;

public partial class Respuestum
{
    public int Idrespuesta { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripción { get; set; }

    public string PorcentajeMtigación { get; set; } = null!;

    public int ValorMejorasMonetario { get; set; }

    public int TiempoRespuesta { get; set; }

    public virtual ICollection<Dañosformulario> DañosformularioIddañosimportantes { get; set; } = new List<Dañosformulario>();
}
