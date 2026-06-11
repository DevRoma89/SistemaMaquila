using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace SistemaMaquila.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanificadorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlanificadorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("simular-cut")]
        public async Task<IActionResult> SimularCutNumber([FromQuery] int prendaId, [FromQuery] int cantidadTotal, [FromQuery] int lineaId, [FromQuery] DateTime fechaInicio)
        {
            // 1. Cargar datos necesarios de la BD mapeando toda tu estructura relacional
            // Usamos ThenInclude para saltar de OperacionPrenda hacia la Operacion base
            var prenda = await _context.Prendas
                .Include(p => p.Operaciones)
                    .ThenInclude(op => op.Operacion)
                .FirstOrDefaultAsync(p => p.Id == prendaId);

            var linea = await _context.Lineas
                .Include(l => l.Empleados)
                .FirstOrDefaultAsync(l => l.Id == lineaId);

            if (prenda == null || linea == null) return BadRequest("Datos de entrada inválidos.");

            // MODIFICACIÓN: Sumamos el SAMReal desde tu clase OperacionPrenda
            decimal samTotalPrenda = prenda.Operaciones.Sum(op => op.SAMReal);
            if (samTotalPrenda == 0) return BadRequest("La prenda no tiene operaciones o el SAM es cero.");

            // 2. Definir variables del algoritmo de consumo de tiempo cronológico
            int jornadaMinutos = 480;
            decimal eficiencia = linea.EficienciaHistorica;
            decimal minutosDisponiblesPorDiaLinea = (linea.Empleados.Count * jornadaMinutos) * eficiencia;

            decimal unidadesPendientes = cantidadTotal;
            double diasCalendarioContados = 0;
            DateTime fechaEvaluar = fechaInicio;

            while (unidadesPendientes > 0)
            {
                if (fechaEvaluar.DayOfWeek == DayOfWeek.Sunday)
                {
                    fechaEvaluar = fechaEvaluar.AddDays(1);
                    continue;
                }

                // Simulación de carga previa en la línea
                decimal minutosYaOcupadosHoy = await _context.ProgracionDiaria
                    .Where(p => p.LineaId == lineaId && p.Fecha.Date == fechaEvaluar.Date)
                    .SumAsync(p => p.CantidadObjetivo * samTotalPrenda);

                decimal minutosDisponiblesHoy = minutosDisponiblesPorDiaLinea - minutosYaOcupadosHoy;

                if (minutosDisponiblesHoy > 0)
                {
                    if (unidadesPendientes == cantidadTotal)
                    {
                        decimal minutosSetupPlanta = prenda.TiempoCambioLineaMinutos * linea.Empleados.Count * eficiencia;
                        minutosDisponiblesHoy -= minutosSetupPlanta;
                    }

                    decimal capacidadPiezasHoy = minutosDisponiblesHoy / samTotalPrenda;

                    if (capacidadPiezasHoy >= unidadesPendientes)
                    {
                        double fraccionDia = (double)(unidadesPendientes / (minutosDisponiblesPorDiaLinea / samTotalPrenda));
                        diasCalendarioContados += fraccionDia;
                        unidadesPendientes = 0;
                    }
                    else
                    {
                        unidadesPendientes -= capacidadPiezasHoy;
                        diasCalendarioContados += (double)(minutosDisponiblesHoy / minutosDisponiblesPorDiaLinea);
                    }
                }

                fechaEvaluar = fechaEvaluar.AddDays(1);
            }

            // 3. CÁLCULO DE MAQUINARIA FÍSICA NECESARIA (Adaptado a tus nuevas clases)
            double piezasPorHoraObjetivo = (cantidadTotal / (diasCalendarioContados * 8));

            // Agrupamos navegando desde OperacionPrenda -> Operacion -> TipoMaquinaId
            var maquinasRequeridas = prenda.Operaciones
                .GroupBy(op => op.Operacion.TipoMaquinaId)
                .Select(grupo => new {
                    TipoMaquinaId = grupo.Key,
                    // Usamos el SAMReal que pertenece a la combinación Prenda-Operación
                    SumaSamDeEsteTipo = grupo.Sum(op => op.SAMReal),
                    MaquinasTeoricas = (piezasPorHoraObjetivo * (double)grupo.Sum(op => op.SAMReal)) / (60.0 * (double)eficiencia),
                })
                .Select(m => new {
                    m.TipoMaquinaId,
                    m.MaquinasTeoricas,
                    CantidadFisicaEntera = (int)Math.Ceiling(m.MaquinasTeoricas)
                }).ToList();

            // 4. Retornar el informe completo de la simulación
            return Ok(new
            {
                CutNumberSimulado = "Simulación Temporal",
                DiasProduccionTotales = Math.Round(diasCalendarioContados, 1),
                FechaEstimadaFin = fechaEvaluar.AddDays(-1).ToString("yyyy-MM-dd"),
                MaquinariaFisicaExigida = maquinasRequeridas
            });
        }
    }
}
