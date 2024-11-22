using System;
using System.Collections.Generic;

namespace RIESGOS_Y_RESPUESTAS.Models;

public partial class RegistroDeDato
{
    public int IdregistroDeDatos { get; set; }

    public string? DatoIdusuario { get; set; }

    public DateTime? DatoTablaRiesgos { get; set; }

    public DateTime? DatoCierre { get; set; }
}
