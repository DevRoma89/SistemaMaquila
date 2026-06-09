using SistemaMaquila.Shared.Entidades.EmpleadoFolder;
using SistemaMaquila.Shared.Entidades.OperacionFolder;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.HabilidadEmpleadoFolder
{
    public class HabilidadEmpleadoGetDTO
    {
        public int Id { get; set; } 
        public string Empleado { get; set; }
        public int OperacionId { get; set; }
        public string Operacion { get; set; }
        public decimal EficienciaSocio { get; set; }
    
        public static HabilidadEmpleadoGetDTO EntityToDto(HabilidadEmpleado ent)
        {

            return new HabilidadEmpleadoGetDTO
            {
                Id = ent.Id,
                Empleado = $"{ent.Empleado.Nombre} {ent.Empleado.Apellido}",
                OperacionId = ent.OperacionId,
                Operacion = ent.Operacion.Descripcion,
                EficienciaSocio = ent.EficienciaSocio
            }; 

        }
    
    }


}
