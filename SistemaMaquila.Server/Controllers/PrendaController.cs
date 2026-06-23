using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMaquila.Shared.Entidades.OperacionPrendaFolder;
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
                                .Include(x=>x.Operaciones)
                                .ThenInclude(x=>x.Operacion.TipoMaquina)
                                .Where(x => x.Visible == true)
                                .Select(x => PrendaGetDTO.EntityToDto(x))
                                .ToListAsync();

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PrendaGetDTO>> ObtenerFichaTecnica([FromRoute]int id)
        {
            var resultado = await context.Prendas
                .Include(p => p.Operaciones)
                .ThenInclude(x=>x.Operacion)
                .FirstOrDefaultAsync(x => x.Id == id); 
             
            if (resultado == null) return NotFound("Prenda no encontrada");
             
            return Ok( FichaTecnicaGetDTO.EntityToDto(resultado) );
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PrendaPutDTO dto)
        {
            if (id != dto.Id) return BadRequest("El Id no coincide.");
            var ent = await context.Prendas.FirstOrDefaultAsync(x => x.Id == id);
            if (ent == null) return NotFound("Prenda no encontrada.");

            ent.Nombre = dto.Nombre.ToUpper();
            ent.Codigo = dto.Codigo.ToUpper();
            ent.TiempoCambioLineaMinutos = dto.TiempoCambioLineaMinutos;
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ent = await context.Prendas.FirstOrDefaultAsync(x => x.Id == id);
            if (ent == null) return NotFound();
            ent.Visible = false;
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}
