using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
