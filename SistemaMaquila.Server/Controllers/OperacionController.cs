using Microsoft.AspNetCore.Mvc;
using SistemaMaquila.Shared.Entidades.TipoMaquinaFolder;
using SistemaMaquila.Shared.Servicios;
using Microsoft.EntityFrameworkCore;
using SistemaMaquila.Shared.Entidades.OperacionFolder;

namespace SistemaMaquila.Server.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class OperacionController : ControllerBase
    {

        private readonly AppDbContext context;

        public OperacionController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<OperacionGetDTO>>> Get()
        {
            return await context.Operaciones
                                .Include(x=>x.TipoMaquina)
                                .Where(x => x.Visible == true)
                                .Select(x => OperacionGetDTO.EntityToDto(x))
                                .ToListAsync();

        }

        [HttpPost]
        public async Task<ActionResult<Operacion>> Post([FromBody] OperacionPostDTO dto)
        {

            if (Validador.CampoTexto(dto.Descripcion))
            {
                return BadRequest("No puede ingresar una Descripcion Vacia");
            }

            if (Validador.CampoNumerico(dto.SAMEstimado))
            {
                return BadRequest("No puede ingresar un SAM menor que 0");
            }

            var existeNombre = await context.Operaciones.AnyAsync(x => x.Descripcion == dto.Descripcion.ToUpper());

            if (existeNombre)
            {
                return NotFound("Ya hay una registro con esa Descripcion");
            }

            var existeMaquina = await context.TipoMaquinas.AnyAsync(x => x.Id == dto.TipoMaquinaId);

            if (existeNombre)
            {
                return NotFound("Ya hay una registro con esa Descripcion");
            }

            var ent = OperacionPostDTO.DtoToEntity(dto);

            context.Operaciones.Add(ent);

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
