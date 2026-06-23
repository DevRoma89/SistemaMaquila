using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMaquila.Shared.Entidades.EmpleadoFolder;
using SistemaMaquila.Shared.Entidades.HabilidadEmpleadoFolder;
using SistemaMaquila.Shared.Servicios;

namespace SistemaMaquila.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class HabilidadEmpleadoController : ControllerBase
    {

        private readonly AppDbContext context;

        public HabilidadEmpleadoController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<HabilidadEmpleadoGetDTO>>> Get()
        {
            return await context.HabilidadesEmpleados
                                .Include(x => x.Empleado)
                                .Include(x => x.Operacion)
                                .Where(x => x.Visible == true)
                                .Select(x => HabilidadEmpleadoGetDTO.EntityToDto(x))
                                .ToListAsync();

        }

        [HttpGet("por-empleado/{empleadoId:int}")]
        public async Task<ActionResult<List<HabilidadEmpleadoGetDTO>>> GetPorEmpleado([FromRoute] int empleadoId)
        {
            return await context.HabilidadesEmpleados
                .Include(e => e.Empleado)
                .Include(h => h.Operacion)
                    .ThenInclude(o => o.TipoMaquina)
                .Where(h => h.EmpleadoId == empleadoId && h.Visible)
                .Select(h => HabilidadEmpleadoGetDTO.EntityToDto(h))
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<HabilidadEmpleado>> Post([FromBody] HabilidadEmpleadoPostDTO dto)
        {

            var existeEmpleado = await context.Empleados.AnyAsync(x => x.Id == dto.EmpleadoId);

            if (!existeEmpleado)
            {
                return NotFound("No se encontro un Empleado con ese Id");
            }

            var existeOperacion = await context.Operaciones.AnyAsync(x => x.Id == dto.OperacionId);

            if (!existeOperacion)
            {
                return NotFound("No se encontro una Operacion con ese Id");
            }

            var ent = HabilidadEmpleadoPostDTO.DtoToEntity(dto);

            context.HabilidadesEmpleados.Add(ent);

            await context.SaveChangesAsync();

            return Ok(ent);


        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] HabilidadEmpleadoPutDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El Id no coincide");
            }
               
            var ent = await context.HabilidadesEmpleados.FirstOrDefaultAsync(x => x.Id == id);

            if (ent == null)
            {
                return NotFound("No se encontro el Empleado");
            }

            ent.EficienciaSocio = dto.EficienciaSocio; 

            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var ent = await context.HabilidadesEmpleados.FirstOrDefaultAsync(x => x.Id == id);

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
