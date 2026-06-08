using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMaquila.Shared.Entidades.EmpleadoFolder;

namespace SistemaMaquila.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadoController:ControllerBase
    {

        private readonly AppDbContext context;

        public EmpleadoController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmpleadoGetDTO>>> Get()
        {
                return await context.Empleados
                                    .Include(x => x.Linea)
                                    .Where(x=>x.Visible == true)
                                    .Select( x  => EmpleadoGetDTO.EntityToDto(x))
                                    .ToListAsync();

        }

        [HttpPost]
        public async Task<ActionResult<Empleado>> Post([FromBody] EmpleadoPostDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Nombre) || string.IsNullOrWhiteSpace(dto.Nombre)) 
            {
                return BadRequest("No puede ingresar un nombre vacio");
            }

            if (string.IsNullOrEmpty(dto.Apellido) || string.IsNullOrWhiteSpace(dto.Apellido)) 
            {
                return BadRequest("No puede ingresar un apellido vacio");
            }

            if ()
            {
                
            }    
            
            var existeLinea = await context.Lineas.AnyAsync(x=>x.Id == dto.LineaId);

            if(!existeLinea)
            {
                return NotFound("No se encontro una Linea con ese Id"); 
            };
            


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
