using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.ProgramacionDiariaFolder
{
    // PlanificacionDiariaResultDTO.cs
    public class PlanificacionDiariaResultDTO
    {
        public PrendaInfoDTO Prenda { get; set; } = new();
        public LineaInfoDTO Linea { get; set; } = new();
        public SamInfoDTO SAM { get; set; } = new();
        public CostosInfoDTO Costos { get; set; } = new();
        public CapacidadInfoDTO Capacidad { get; set; } = new();
        public MaquinariaInfoDTO Maquinaria { get; set; } = new();
    }

    public class PrendaInfoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public int TotalOperaciones { get; set; }
    }

    public class LineaInfoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal EficienciaHistorica { get; set; }
        public int CantidadEmpleados { get; set; }
    }

    public class SamInfoDTO
    {
        public decimal SamTotalPuro { get; set; }
        public decimal SamAjustadoEficiencia { get; set; }
    }

    public class CostosInfoDTO
    {
        public decimal CostoMinutoPromedioOperario { get; set; }
        public decimal CostoMinutoRealPlanta { get; set; }
        public decimal CostoPorUnidad { get; set; }
    }

    public class CapacidadInfoDTO
    {
        public int JornadaMinutos { get; set; }
        public decimal MinutosDisponiblesDia { get; set; }
        public decimal UnidadesEstimadasPorDia { get; set; }
    }

    public class MaquinariaInfoDTO
    {
        public bool LineaPuedeProducirPrenda { get; set; }
        public List<DetalleMaquinaDTO> DetallePorTipo { get; set; } = new();
    }

    public class DetalleMaquinaDTO
    {
        public int TipoMaquinaId { get; set; }
        public string NombreMaquina { get; set; } = string.Empty;
        public int MaquinasNecesarias { get; set; }
        public int MaquinasDisponibles { get; set; }
        public bool CubreCapacidad { get; set; }
    }
}
