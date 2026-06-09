using SistemaMaquila.Shared.Entidades.EmpleadoFolder;
using SistemaMaquila.Shared.Entidades.OperacionFolder;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.HabilidadEmpleadoFolder
{
    public class HabilidadEmpleadoPostDTO
    {  
        public int EmpleadoId { get; set; } 
        public int OperacionId { get; set; } 
        public decimal EficienciaSocio { get; set; }

        public static HabilidadEmpleado DtoToEntity(HabilidadEmpleadoPostDTO dto)
        {

            return new HabilidadEmpleado
            {
                EmpleadoId = dto.EmpleadoId,
                OperacionId = dto.OperacionId,
                EficienciaSocio = dto.EficienciaSocio
            }; 
            
        }

    }
}
