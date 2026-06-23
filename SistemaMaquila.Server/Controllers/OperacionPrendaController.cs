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

        [HttpGet("por-prenda/{prendaId}")]
        public async Task<ActionResult<List<OperacionPrendaGetDTO>>> GetPorPrenda(int prendaId)
        {
            return await context.OperacionPrendas
                .Include(op => op.Operacion)
                    .ThenInclude(o => o.TipoMaquina)
                .Where(op => op.PrendaId == prendaId && op.Visible)
                .OrderBy(op => op.OrdenSecuencia)
                .Select(op => OperacionPrendaGetDTO.EntityToDto(op))
                .ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OperacionPrendaPostDTO dto)
        {
            // Validar que no se duplique la misma operación en la misma prenda
            var existe = await context.OperacionPrendas
                .AnyAsync(op => op.PrendaId == dto.PrendaId
                             && op.OperacionId == dto.OperacionId
                             && op.Visible);
            if (existe) return BadRequest("Esa operación ya está asignada a la prenda.");

            var ent = new OperacionPrenda
            {
                PrendaId = dto.PrendaId,
                OperacionId = dto.OperacionId,
                OrdenSecuencia = dto.OrdenSecuencia,
                SAMReal = dto.SAMReal,
                Visible = true
            };
            context.OperacionPrendas.Add(ent);
            await context.SaveChangesAsync();
            return Ok(ent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] OperacionPrendaPutDTO dto)
        {
            if (id != dto.Id) return BadRequest("El Id no coincide.");
            var ent = await context.OperacionPrendas.FirstOrDefaultAsync(x => x.Id == id);
            if (ent == null) return NotFound();

            ent.OperacionId = dto.OperacionId;
            ent.OrdenSecuencia = dto.OrdenSecuencia;
            ent.SAMReal = dto.SAMReal;
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ent = await context.OperacionPrendas.FirstOrDefaultAsync(x => x.Id == id);
            if (ent == null) return NotFound();
            ent.Visible = false;
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}
