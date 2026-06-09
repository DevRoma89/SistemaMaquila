using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMaquila.Shared.Entidades.PrendaFolder;
using SistemaMaquila.Shared.Entidades.TipoMaquinaFolder;
using SistemaMaquila.Shared.Servicios;

namespace SistemaMaquila.Server.Controllers
{
      
    [ApiController]
    [Route("api/[controller]")]
    public class PrendaController : ControllerBase
    {

        private readonly AppDbContext context;

        public PrendaController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<PrendaGetDTO>>> Get()
        {
            return await context.Prendas
                                .Where(x => x.Visible == true)
                                .Select(x => PrendaGetDTO.EntityToDto(x))
                                .ToListAsync();

        }

        [HttpPost]
        public async Task<ActionResult<Prenda>> Post([FromBody] PrendaPostDTO dto)
        {

            if (Validador.CampoTexto(dto.Nombre))
            {
                return BadRequest("No puede ingresar un Nombre Vacio");
            }

            if (Validador.CampoTexto(dto.Codigo))
            {
                return BadRequest("No puede ingresar un Codigo Vacio");
            }
               
            var ent = PrendaPostDTO.DtoToEntity(dto);

            context.Prendas.Add(ent);

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
