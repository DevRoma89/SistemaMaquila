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
                                .Include(x=>x.Linea)
                                .Include(x=>x.TipoMaquina)
                                .Where(x => x.Visible == true)
                                .Select(x => InventarioLineaGetDTO.EntityToDto(x))
                                .ToListAsync();

        }
        [HttpGet("{lineaId:int}")]
        public async Task<ActionResult<List<InventarioLineaGetDTO>>> Get([FromRoute] int lineaId )
        {
             
            return await context.InventarioLineas
                                .Include(x=>x.Linea)
                                .Include(x=>x.TipoMaquina)
                                .Where(x => x.Visible == true && x.LineaId == lineaId)
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

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] InventarioLineaPostDTO dto)
        { 
               
            var ent = await context.InventarioLineas.FirstOrDefaultAsync(x => x.Id == id);

            if (ent == null)
            {
                return NotFound("No se encontro el Id de la Linea");
            }
            
            ent.CantidadDisponible = dto.CantidadDisponible;

            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var ent = await context.InventarioLineas.FirstOrDefaultAsync(x => x.Id == id);

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
