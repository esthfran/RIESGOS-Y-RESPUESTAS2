using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace RIESGOS_Y_RESPUESTAS.Models;

public partial class Usuario
{
    public int Idusuario { get; set; }

    public string? Usuario1 { get; set; }

    public string? Correo { get; set; }

    public string? Contraseña { get; set; }

    public string? Roles { get; set; }

    public DateTime FechaRegistro { get; set; }
}
