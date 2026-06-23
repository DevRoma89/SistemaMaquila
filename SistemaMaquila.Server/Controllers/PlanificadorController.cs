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

        [HttpGet("sam-prenda")]
        public async Task<IActionResult> CalcularSamPrenda(
    [FromQuery] int prendaId,
    [FromQuery] int lineaId)
        {
            // 1. Cargar prenda con sus operaciones y los tipos de máquina
            var prenda = await _context.Prendas
                .Include(p => p.Operaciones)
                    .ThenInclude(op => op.Operacion)
                        .ThenInclude(o => o.TipoMaquina)
                .FirstOrDefaultAsync(p => p.Id == prendaId && p.Visible);

            if (prenda == null)
                return NotFound("Prenda no encontrada o inactiva.");

            if (!prenda.Operaciones.Any(op => op.Visible))
                return BadRequest("La prenda no tiene operaciones activas definidas.");

            // 2. Cargar línea con sus empleados e inventario de máquinas
            var linea = await _context.Lineas
                .Include(l => l.Empleados.Where(e => e.Visible))
                .FirstOrDefaultAsync(l => l.Id == lineaId && l.Visible);

            if (linea == null)
                return NotFound("Línea no encontrada o inactiva.");

            if (!linea.Empleados.Any())
                return BadRequest("La línea no tiene empleados activos asignados.");

            // 3. Cargar inventario de máquinas de la línea
            var inventarioLinea = await _context.InventarioLineas
                .Include(i => i.TipoMaquina)
                .Where(i => i.LineaId == lineaId && i.Visible)
                .ToListAsync();

            // 4. Calcular SAM total puro (suma de SAMReal de cada OperacionPrenda activa)
            var operacionesActivas = prenda.Operaciones
                .Where(op => op.Visible)
                .OrderBy(op => op.OrdenSecuencia)
                .ToList();

            decimal samTotalPuro = operacionesActivas.Sum(op => op.SAMReal);

            // 5. Ajustar SAM por eficiencia de la línea
            // A menor eficiencia, más minutos reales necesita la línea para producir 1 unidad
            decimal eficiencia = linea.EficienciaHistorica > 0 ? linea.EficienciaHistorica : 0.01m;
            decimal samAjustadoPorEficiencia = samTotalPuro / eficiencia;

            // 6. Calcular costo por unidad
            // Costo minuto combinado de todos los empleados de la línea
            decimal costoMinutoCombinado = linea.Empleados.Sum(e => e.CostoMinutoBase);
            decimal costoMinutoPromedioOperario = costoMinutoCombinado / linea.Empleados.Count;
            // El costo real por minuto productivo ya ajusta la ineficiencia
            decimal costoMinutoRealPlanta = costoMinutoPromedioOperario / eficiencia;
            // Costo de producir 1 unidad = minutos reales × costo real por minuto
            decimal costoPorUnidad = samAjustadoPorEficiencia * costoMinutoRealPlanta;

            // 7. Capacidad diaria estimada de la línea para esta prenda
            const int jornadaMinutos = 480;
            decimal minutosTotalesDisponiblesDia = linea.Empleados.Count * jornadaMinutos * eficiencia;
            decimal unidadesPorDia = samTotalPuro > 0
                ? minutosTotalesDisponiblesDia / samTotalPuro
                : 0;

            // 8. Validar disponibilidad de máquinas por tipo
            // Agrupamos las operaciones por TipoMaquina para saber cuántas se necesitan
            double piezasPorHora = (double)unidadesPorDia / 8.0;

            var maquinasRequeridas = operacionesActivas
                .GroupBy(op => op.Operacion.TipoMaquinaId)
                .Select(grupo =>
                {
                    var tipoMaquinaId = grupo.Key;
                    var nombreMaquina = grupo.First().Operacion.TipoMaquina?.Nombre ?? "Desconocido";
                    var samDelTipo = grupo.Sum(op => op.SAMReal);
                    double maquinasTeoricas = (piezasPorHora * (double)samDelTipo) / (60.0 * (double)eficiencia);
                    int maquinasNecesarias = (int)Math.Ceiling(maquinasTeoricas);

                    var stockLinea = inventarioLinea
                        .FirstOrDefault(i => i.TipoMaquinaId == tipoMaquinaId);
                    int maquinasDisponibles = stockLinea?.CantidadDisponible ?? 0;

                    return new
                    {
                        TipoMaquinaId = tipoMaquinaId,
                        NombreMaquina = nombreMaquina,
                        MaquinasNecesarias = maquinasNecesarias,
                        MaquinasDisponibles = maquinasDisponibles,
                        // true = la línea puede cubrir la demanda de ese tipo de máquina
                        CubreCapacidad = maquinasDisponibles >= maquinasNecesarias
                    };
                })
                .ToList();

            bool lineaPuedeProducirPrenda = maquinasRequeridas.All(m => m.CubreCapacidad);

            // 9. Respuesta completa
            return Ok(new
            {
                Prenda = new
                {
                    prenda.Id,
                    prenda.Nombre,
                    prenda.Codigo,
                    TotalOperaciones = operacionesActivas.Count
                },
                Linea = new
                {
                    linea.Id,
                    linea.Nombre,
                    linea.EficienciaHistorica,
                    CantidadEmpleados = linea.Empleados.Count
                },
                SAM = new
                {
                    // Minutos teóricos si la línea fuera 100% eficiente
                    SamTotalPuro = Math.Round(samTotalPuro, 4),
                    // Minutos reales que consume la línea por unidad
                    SamAjustadoEficiencia = Math.Round(samAjustadoPorEficiencia, 4),
                },
                Costos = new
                {
                    CostoMinutoPromedioOperario = Math.Round(costoMinutoPromedioOperario, 4),
                    CostoMinutoRealPlanta = Math.Round(costoMinutoRealPlanta, 4),
                    // Cuánto cuesta a la línea producir 1 unidad de esta prenda
                    CostoPorUnidad = Math.Round(costoPorUnidad, 4)
                },
                Capacidad = new
                {
                    JornadaMinutos = jornadaMinutos,
                    MinutosDisponiblesDia = Math.Round(minutosTotalesDisponiblesDia, 2),
                    UnidadesEstimadasPorDia = Math.Round(unidadesPorDia, 1)
                },
                Maquinaria = new
                {
                    LineaPuedeProducirPrenda = lineaPuedeProducirPrenda,
                    DetallePorTipo = maquinasRequeridas
                }
            });
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
