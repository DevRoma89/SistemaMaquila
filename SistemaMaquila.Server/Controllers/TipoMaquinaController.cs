using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using SistemaMaquila.Shared.Entidades.LineaFolder;
using SistemaMaquila.Shared.Entidades.TipoMaquinaFolder;
using SistemaMaquila.Shared.Servicios;

namespace SistemaMaquila.Server.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class TipoMaquinaController : ControllerBase
    {

        private readonly AppDbContext context;

        public TipoMaquinaController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoMaquinaGetDTO>>> Get()
        {
            return await context.TipoMaquinas
                                .Where(x => x.Visible == true)
                                .Select(x => TipoMaquinaGetDTO.EntityToDto(x))
                                .ToListAsync();

        }

        [HttpPost]
        public async Task<ActionResult<TipoMaquina>> Post([FromBody] TipoMaquinaPostDTO dto)
        {

            if (Validador.CampoTexto(dto.Nombre))
            {
                return BadRequest("No puede ingresar un Nombre Vacio");
            }
              
            var existeNombre = await context.TipoMaquinas.AnyAsync(x => x.Nombre == dto.Nombre.ToUpper());

            if (existeNombre)
            {
                return NotFound("Ya hay una registro con ese Nombre");
            }

            var ent = TipoMaquinaPostDTO.DtoToEntity(dto);

            context.TipoMaquinas.Add(ent);

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
