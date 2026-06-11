using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.EmpleadoFolder
{
    public class EmpleadoPutDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public decimal CostoMinutoBase { get; set; }
        public int LineaId { get; set; }
        public static void DtoToEntity(EmpleadoPutDTO dto, Empleado entity)
        {
            entity.Nombre = dto.Nombre.ToUpper();
            entity.Apellido = dto.Apellido.ToUpper();
            entity.CostoMinutoBase = dto.CostoMinutoBase;
            entity.LineaId = dto.LineaId;
        }

    }
}
