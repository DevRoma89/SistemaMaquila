using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.EmpleadoFolder
{
    public class EmpleadoPostDTO
    {

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public decimal CostoMinutoBase { get; set; }
        public int LineaId { get; set; }
        public static Empleado DtoToEntity(EmpleadoPostDTO dto)
        {
            return new Empleado
            {

                Nombre = dto.Nombre.ToUpper(),
                Apellido = dto.Apellido.ToUpper(),
                CostoMinutoBase = dto.CostoMinutoBase,
                LineaId = dto.LineaId

            }; 
        }
    }
}
