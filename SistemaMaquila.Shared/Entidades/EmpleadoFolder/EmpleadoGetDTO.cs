using SistemaMaquila.Shared.Entidades.LineaFolder;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.EmpleadoFolder
{
    public class EmpleadoGetDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public decimal CostoMinutoBase { get; set; }
        public int LineaId { get; set; }
        public string Linea { get; set; } 


        public static EmpleadoGetDTO EntityToDto(Empleado ent)
        {

            return new EmpleadoGetDTO
            {
                Id = ent.Id,
                Nombre = ent.Nombre,
                Apellido = ent.Apellido,
                CostoMinutoBase = ent.CostoMinutoBase,
                LineaId = ent.LineaId,
                Linea = ent.Linea.Nombre
            };

        }

    }
}
