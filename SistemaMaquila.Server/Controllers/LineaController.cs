using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMaquila.Shared.Entidades.EmpleadoFolder;
using SistemaMaquila.Shared.Entidades.HabilidadEmpleadoFolder;
using SistemaMaquila.Shared.Entidades.LineaFolder;
using SistemaMaquila.Shared.Servicios;

namespace SistemaMaquila.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LineaController : ControllerBase
    {

        private readonly AppDbContext context;

        public LineaController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<LineaGetDTO>>> Get()
        {
            return await context.Lineas
                                .Where(x => x.Visible == true)
                                .Select(x => LineaGetDTO.EntityToDto(x))
                                .ToListAsync();

        }

        [HttpGet("{id}/metricas-costo")]
        public async Task<IActionResult> ObtenerMetricasCosto(int id)
        {
            var linea = await context.Lineas
                .Include(l => l.Empleados)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (linea == null) return NotFound("Línea no encontrada");
            if (!linea.Empleados.Any()) return BadRequest("La línea no tiene operarios asignados");

            // 1. Sumamos el costo por minuto base de todos los operarios en la línea
            decimal costoMinutoCombinado = linea.Empleados.Sum(e => e.CostoMinutoBase);

            // 2. Sacamos el promedio teórico por operario
            decimal costoMinutoPromedioTeorico = costoMinutoCombinado / linea.Empleados.Count;

            // 3. Ajustamos por la ineficiencia (A menor eficiencia, más caro el minuto real productivo)
            // Evitamos división por cero si la eficiencia está en 0
            decimal eficiencia = linea.EficienciaHistorica > 0 ? linea.EficienciaHistorica : 0.01m;
            decimal costoMinutoRealPlanta = costoMinutoPromedioTeorico / eficiencia;

            return Ok(new
            {
                Linea = linea.Nombre,
                CantidadOperarios = linea.Empleados.Count,
                EficienciaReal = linea.EficienciaHistorica,
                CostoMinutoTeoricoPromedio = Math.Round(costoMinutoPromedioTeorico, 4),
                CostoMinutoRealAjustado = Math.Round(costoMinutoRealPlanta, 4)
            });
        }

        [HttpPost]
        public async Task<ActionResult<Linea>> Post([FromBody] LineaPostDTO dto)
        {

            if (Validador.CampoTexto(dto.Nombre))
            {
                return BadRequest("No puede ingresar un Nombre Vacio");
            }

            if (Validador.CampoNumerico(dto.EficienciaHistorica))
            {
                return BadRequest("Debe ingresar un numero mayor a 0");
            }

            var existeNombre = await context.Lineas.AnyAsync(x => x.Nombre == dto.Nombre.ToUpper());

            if (existeNombre)
            {
                return NotFound("Ya hay una registro con ese Nombre");
            }

            var ent = LineaPostDTO.DtoToEntity(dto);

            context.Lineas.Add(ent);

            await context.SaveChangesAsync();   

            return Ok(ent);


        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] LineaPutDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El Id no coincide");
            }

            if (Validador.CampoTexto(dto.Nombre))
            {
                return BadRequest("No puede ingresar un nombre vacio");
            }
             
            if (Validador.CampoNumerico(dto.EficienciaHistorica))
            {
                return BadRequest("Debe ingresar un Costo mayor a 0");
            }
              
            var ent = await context.Lineas.FirstOrDefaultAsync(x => x.Id == id);

            if (ent == null)
            {
                return NotFound("No se encontro el Empleado");
            }

            LineaPutDTO.DtoToEntity(dto, ent);

            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var ent = await context.Lineas.FirstOrDefaultAsync(x => x.Id == id);

            if (ent == null)
            {
                return NotFound();
            }

            ent.Visible = false;

            await context.SaveChangesAsync();

            return Ok();
        }

    }
}
