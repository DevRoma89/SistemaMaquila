using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using SistemaMaquila.Shared.Entidades.InventarioLineaFolder;
using SistemaMaquila.Shared.Entidades.LineaFolder;
using SistemaMaquila.Shared.Servicios;

namespace SistemaMaquila.Server.Controllers
{
    

    [ApiController]
    [Route("api/[controller]")]
    public class InventarioLineaController : ControllerBase
    {

        private readonly AppDbContext context;

        public InventarioLineaController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<InventarioLineaGetDTO>>> Get()
        {
            return await context.InventarioLineas
                                .Where(x => x.Visible == true)
                                .Select(x => InventarioLineaGetDTO.EntityToDto(x))
                                .ToListAsync();

        }

        [HttpPost]
        public async Task<ActionResult<InventarioLinea>> Post([FromBody] InventarioLineaPostDTO dto)
        {
             

            if (Validador.CampoNumerico(dto.CantidadDisponible))
            {
                return BadRequest("Debe ingresar un numero mayor a 0");
            }

            var existeLinea = await context.Lineas.AnyAsync(x => x.Id == dto.LineaId);

            if (!existeLinea)
            {
                return NotFound("No existe una Linea con ese ID");
            }

            var existeMaquina = await context.TipoMaquinas.AnyAsync(x => x.Id == dto.TipoMaquinaId);
            
            if (!existeMaquina)
            {
                return NotFound("No existe una Linea con ese ID");
            }

            var ent = InventarioLineaPostDTO.DtoToEntity(dto);

            context.InventarioLineas.Add(ent);

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
