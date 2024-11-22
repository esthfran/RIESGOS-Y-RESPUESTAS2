using System;
using System.Collections.Generic;

namespace RIESGOS_Y_RESPUESTAS.Models;

public partial class Riesgo
{
    public int Idreportes { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal NivelAmenaza { get; set; }

    public string? Detalles { get; set; }

    public int? AreaMunicipalidad { get; set; }

    public string Daños { get; set; } = null!;

    public byte EstadoRiesgo { get; set; }

    public string TipoRiesgo { get; set; } = null!;

    public decimal Impacto { get; set; }

    public virtual MunicipalidadIqq? AreaMunicipalidadNavigation { get; set; }

    public virtual ICollection<Dañosformulario> DañosformularioIddañosimportantes { get; set; } = new List<Dañosformulario>();

    public string GetNivelAmenazaDescription()
    {
        return NivelAmenaza switch
        {
            1 => "Muy Bajo",
            2 => "Bajo",
            3 => "Medio",
            4 => "Alto",
            5 => "Muy Alto",
            _ => "Desconocido"
        };
    }

    public string GetImpactoDescription()
    {
        return Impacto switch
        {
            0 => "Nulo - Irrelevante a efectos prácticos",
            1 => "Bajo (1) - Daño menor",
            2 => "Bajo (2) - Daño menor",
            3 => "Medio (3) - Daño importante",
            4 => "Medio (4) - Daño importante",
            5 => "Medio (5) - Daño importante",
            6 => "Alto (6) - Daño grave",
            7 => "Alto (7) - Daño grave",
            8 => "Alto (8) - Daño grave",
            9 => "Muy alto (9) - Daño muy grave",
            10 => "Extremo (10) - Daño extremadamente grave",
            _ => "Desconocido"
        };
    }

    public string GetEstadoRiesgoDescription()
    {
        return EstadoRiesgo switch
        {
            0 => "DESCONOCIDO",
            1 => "Nuevo",
            2 => "activo",
            3 => "pendiente de evaluación",
            4 => "en proceso de solicitud",
            5 => "mitigado",
            6 => "riesgoso",
            7 => "peligroso",
            _ => "DESCONOCIDO"
        };
    }
}
