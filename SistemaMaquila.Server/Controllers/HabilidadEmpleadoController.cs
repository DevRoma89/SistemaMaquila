using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMaquila.Shared.Entidades.EmpleadoFolder;
using SistemaMaquila.Shared.Entidades.HabilidadEmpleadoFolder;

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
                                .Include(x=>x.Empleado)
                                .Include(x=>x.Operacion)
                                .Where(x => x.Visible == true)
                                .Select(x => HabilidadEmpleadoGetDTO.EntityToDto(x))
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

        [HttpPut]
        public async Task<ActionResult> Put()
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete()
        {
            return Ok();
        }
    }
}
