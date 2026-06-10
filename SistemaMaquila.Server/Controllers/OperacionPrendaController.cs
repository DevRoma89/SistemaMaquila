using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMaquila.Shared.Entidades.OperacionPrendaFolder;
using SistemaMaquila.Shared.Entidades.PrendaFolder;
using SistemaMaquila.Shared.Servicios;

namespace SistemaMaquila.Server.Controllers
{
    

    [ApiController]
    [Route("api/[controller]")]
    public class OperacionPrendaController : ControllerBase
    {

        private readonly AppDbContext context;

        public OperacionPrendaController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<OperacionPrendaGetDTO>>> Get()
        {
            return await context.OperacionPrendas
                                .Where(x => x.Visible == true)
                                .Select(x => OperacionPrendaGetDTO.EntityToDto(x))
                                .ToListAsync();

        }

        [HttpPost]
        public async Task<ActionResult<OperacionPrenda>> Post([FromBody] OperacionPrendaPostDTO dto)
        {

            if (Validador.CampoNumerico(dto.SAMReal))
            {
                return BadRequest("Debe ingresar un numero mayor a 0");
            }


            var existePrenda = await context.Prendas.AnyAsync(x => x.Id == dto.PrendaId);

            if (!existePrenda)
            {
                return NotFound("No se encontro una prenda con ese Id");
            }

            var existeOperacion = await context.Operaciones.AnyAsync(x => x.Id == dto.OperacionId); 

            if (!existeOperacion)
            {
                return NotFound("No se encontro una operacion con ese Id");
            }

            var ent = OperacionPrendaPostDTO.DtoToEntity(dto);

            context.OperacionPrendas.Add(ent);

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
